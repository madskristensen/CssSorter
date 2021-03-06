﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Css.Extensions;
using Microsoft.CSS.Core;
using Microsoft.CSS.Editor;
using Microsoft.Html.Editor;
using Microsoft.Less.Core;
using Microsoft.Scss.Core;
using Microsoft.Web.Editor;

namespace CssSorter
{
    public class Sorter
    {
        public string[] SortDeclarations(IEnumerable<string> declarations)
        {
            Dictionary<string, string> inlineCommentStorage = new Dictionary<string, string>();
            List<string> filteredDeclarations = new List<string>();

            // How could you do this with clever linq stuff?
            foreach (string attribute in declarations)
            {
                string trimmedAttribute = attribute.Trim();
                Match inlineCommentRegexMatch = Regex.Match(trimmedAttribute, "/\\*.*\\*/");
                if (inlineCommentRegexMatch.Success
                    && inlineCommentRegexMatch.Index > 0)
                {
                    string comment = inlineCommentRegexMatch.Value.Trim();
                    string value = trimmedAttribute.Remove(inlineCommentRegexMatch.Index).Trim();
                    inlineCommentStorage.Add(value, comment);
                    filteredDeclarations.Add(value);
                }
                else
                {
                    filteredDeclarations.Add(attribute.Trim());
                }
            }

            string rule = "div {" + string.Join(Environment.NewLine, filteredDeclarations) + "}";
            CssParser parser = new CssParser();
            StyleSheet sheet = parser.Parse(rule, true);

            var comments = sheet.RuleSets[0].Block.Children.Where(c => c is CComment).Select(c => Environment.NewLine + c.Text);
            var decls = sheet.RuleSets[0].Block.Declarations.Select(d => d.Text);
            var sorted = decls.OrderBy(d => d, new DeclarationComparer());

            List<string> list = new List<string>(Stringify(sorted));
            list.AddRange(comments.OrderBy(c => c));

            var query = from c in list
                        join o in inlineCommentStorage on c equals o.Key into gj
                        from sublist in gj.DefaultIfEmpty()
                        select c + (sublist.Key == null ? string.Empty : ' ' + sublist.Value);

            return query.ToArray();
        }

        private string[] SortDeclarations2(IEnumerable<string> declarations)
        {
            var clean = from d in declarations
                        where !string.IsNullOrWhiteSpace(d)
                        select d.Trim();

            var sorted = clean.OrderBy(d => d, new DeclarationComparer());

            return Stringify(sorted).ToArray();
        }

        public string SortStyleSheet(string css)
        {
            ICssParser parser = new CssParser();
            StyleSheet stylesheet = parser.Parse(css.Trim(), true);

            CssFormatter formatter = new CssFormatter();
            formatter.Options.RemoveLastSemicolon = false;

            StringBuilder sb = new StringBuilder(stylesheet.Text);

            var visitor = new CssItemCollector<RuleBlock>(true);
            stylesheet.Accept(visitor);

            foreach (RuleBlock rule in visitor.Items.Where(r => r.IsValid).Reverse())
            {
                if (rule.Declarations.Count <= 1)
                    continue;

                int start = rule.OpenCurlyBrace.AfterEnd;
                int length = rule.Length - 2;

                string text = formatter.Format(rule.Text).Trim().Trim('}', '{');
                string[] declarations = text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var sorted = SortDeclarations(declarations);

                sb.Remove(start, length);
                sb.Insert(start, string.Join("", sorted));
            }

            return sb.ToString();
        }

        public string SortLess(string less)
        {
            ICssParser parser = CssParserLocator.FindComponent(ContentTypeManager.GetContentType(LessContentTypeDefinition.LessContentType)).CreateParser();
            StyleSheet stylesheet = parser.Parse(less.Trim(), true);

            return PreprocessorSorting(stylesheet);
        }

        public string SortScss(string scss)
        {
            ICssParser parser = CssParserLocator.FindComponent(ContentTypeManager.GetContentType(ScssContentTypeDefinition.ScssContentType)).CreateParser();
            StyleSheet stylesheet = parser.Parse(scss.Trim(), true);

            return PreprocessorSorting(stylesheet);
        }

        private string PreprocessorSorting(StyleSheet stylesheet)
        {
            StringBuilder sb = new StringBuilder(stylesheet.Text);

            var visitor = new CssItemCollector<CssRuleBlock>(true);
            stylesheet.Accept(visitor);

            foreach (var rule in visitor.Items.Where(r => r.IsValid).Reverse())
            {
                if (rule.Children.Count < 2)
                    continue;

                int start = rule.OpenCurlyBrace.AfterEnd;
                int length = rule.Length - 1;

                length = AdjustLength(rule, start, length);

                if (length < 1)
                    continue;

                string text = GetNormalizedText(rule, start, length);

                string[] declarations = text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //.Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();

                var sorted = SortDeclarations2(declarations);

                sb.Remove(start, length - 1);
                sb.Insert(start, string.Join("", sorted));
            }

            return sb.ToString();
        }

        private static string GetNormalizedText(CssRuleBlock rule, int start, int length)
        {
            StringBuilder text = new StringBuilder();

            foreach (ParseItem dec in rule.Children.Where(
                     c =>
                          c is Declaration ||
                          c is Comment ||
                          c is LessMixinReferenceList ||
                          c is LessVariableDeclaration ||
                          c is ScssMixinReference ||
                          c is ScssVariableDeclaration))
            {
                if (dec.Start > start + length)
                    break;

                text.AppendLine(dec.Text);
            }

            return text.ToString();
        }

        private static int AdjustLength(RuleBlock rule, int start, int length)
        {
            var inner = new CssItemCollector<RuleSet>();
            rule.Accept(inner);

            if (inner.Items.Count > 0)
            {
                length = inner.Items[0].Start - start;
            }
            return length;
        }

        private IEnumerable<string> Stringify(IEnumerable<string> declarations)
        {
            List<string> list = new List<string>();

            bool isInMultiLineComment = false;

            foreach (string dec in declarations)
            {
                bool hasCommentStart = dec.Contains("/*");
                bool hasCommentEnd = dec.Contains("*/");
                if (!dec.EndsWith(";") && !hasCommentStart && !hasCommentEnd && !isInMultiLineComment)
                    list.Add(dec + ";");
                else
                    list.Add(dec);
                if (isInMultiLineComment && hasCommentEnd) isInMultiLineComment = false;
                if (hasCommentStart && !hasCommentEnd) isInMultiLineComment = true;
            }

            return list;
        }
    }
}

