using System.Collections.Generic;

namespace CssSorter
{
    public class ValueList
    {
        public static List<string> Values { get; set; }

        static ValueList()
        {
            FillList();
        }

        private static void FillList()
        {
            Values = new List<string>(new string[] {
                 "#",
                 "url",
                 "-moz-",
                 "-webkit-",
                 "-webkit-",
                 "-o-",
                 "-ms-",
                 "-"
            });
        }
    }
}
