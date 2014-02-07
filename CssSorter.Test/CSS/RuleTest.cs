using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test
{
    [TestClass]
    public class RuleTest
    {
        private Sorter _sorter = new Sorter();

        [TestMethod, TestCategory("Rule")]
        public void RuleBasic()
        {
            string input = "div.item { top: 10px; position: relative; }";
            string expected = "div.item {position: relative;top: 10px;}";

            string result = _sorter.SortStyleSheet(input);

            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("Rule")]
        public void RuleWithInlineComments()
        {
            string input = "div.item { top: 10px; /* ostehat */ \r\n position: relative; }";
            string expected = "div.item {position: relative;top: 10px; /* ostehat */}";

            string result = _sorter.SortStyleSheet(input);

            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("Rule")]
        public void RuleMultipleRules()
        {
            string input = "div.item { top: 10px; position: relative; }" + Environment.NewLine + "div.item.last { bottom:10px; }";
            string expected = "div.item {position: relative;top: 10px;}\r\ndiv.item.last { bottom:10px; }";

            string result = _sorter.SortStyleSheet(input);

            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("Rule")]
        public void RuleMultipleRulesWithBlanks()
        {
            string input = "\t" + Environment.NewLine + " \t   " + Environment.NewLine + "div.item { top: 10px; position: relative; }" + Environment.NewLine + "div.item.last { bottom:10px; }";
            string expected = "div.item {position: relative;top: 10px;}\r\ndiv.item.last { bottom:10px; }";

            string result = _sorter.SortStyleSheet(input);

            Assert.AreEqual(expected, result);
        }
    }
}
