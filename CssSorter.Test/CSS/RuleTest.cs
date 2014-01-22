using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test
{
    [TestClass]
    public class RuleTest
    {
        private Sorter _sorter = new Sorter();


        [TestMethod, TestCategory("Rule")]
        public void Rule1()
        {
            string input = "div.item { top: 10px; position: relative; }";
            string expected = "div.item {position: relative;top: 10px;}";

            string result = _sorter.SortStyleSheet(input);

            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("Rule")]
        public void Rule2()
        {
            string input = "div.item { top: 10px; /* ostehat */ \r\n position: relative; }";
            string expected = "div.item {position: relative;top: 10px; /* ostehat */}";

            string result = _sorter.SortStyleSheet(input);

            Assert.AreEqual(expected, result);
        }
    }
}
