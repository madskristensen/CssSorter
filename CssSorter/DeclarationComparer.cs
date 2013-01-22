using System;
using System.Collections.Generic;

namespace CssSorter
{
    public class DeclarationComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            string xOrig = GetPropertyName(x);
            string yOrig = GetPropertyName(y);

            if (xOrig == yOrig)
                return string.Compare(x, y, StringComparison.Ordinal);

            string xNorm = GetNormalizedName(x);
            string yNorm = GetNormalizedName(y);

            // Vendor specifics
            if (x.StartsWith("-") && y.StartsWith("-"))
            {
                if (xNorm == yNorm)
                    return string.Compare(x, y, StringComparison.Ordinal);
                else
                    return xNorm.CompareTo(yNorm);
            }

            if (x.StartsWith("-") && !y.StartsWith("-"))
            {
                if (xNorm == yNorm)
                    return -1;
            }

            if (!x.StartsWith("-") && y.StartsWith("-"))
            {
                if (xNorm == yNorm)
                    return 1;
            }

            // IE hacks
            if (xNorm == yNorm)
            {
                if ((x[0] == '*' || x[0] == '_') && (y[0] != '*' && y[0] != '_'))
                    return 1;

                if ((y[0] == '*' || y[0] == '_') && (x[0] != '*' && x[0] != '_'))
                    return -1;
            }

            //if (xNorm == yNorm || (xNorm == null && yNorm == null))
            //    return 0;

            if (xNorm == null)
                return -1;

            if (yNorm == null)
                return 1;

            // Mixins
            if (xNorm[0] == '.' && yNorm[0] != '0')
                return 1;

            if (yNorm[0] == '.' && xNorm[0] != '0')
                return -1;

            int xPos = PropertyList.Properties.IndexOf(xNorm);
            int yPos = PropertyList.Properties.IndexOf(yNorm);

            return xPos < yPos ? -1 : 1;
        }

        private static string GetPropertyName(string declaration)
        {
            // For Mixins
            if (declaration[0] == '.')
                return declaration;

            // For commented out properties
            int index = declaration.IndexOf(':');

            if (index > -1)
            {
                return declaration.Substring(0, index);//.Replace("");
            }

            return null;
        }

        private static string GetNormalizedName(string declaration)
        {
            // For Mixins
            if (declaration[0] == '.')
                return declaration;

            declaration = declaration.TrimStart('*', '_', ' ');

            int index = declaration.IndexOf(':');

            if (index > -1)
            {
                string name = declaration.Substring(0, index).TrimStart('/', '*', ' ');

                if (name[0] == '-')
                {
                    int ost = name.IndexOf('-', 1) + 1;
                    name = ost > 0 ? name.Substring(ost) : name;
                }

                return name;
            }

            return null;
        }
    }
}
