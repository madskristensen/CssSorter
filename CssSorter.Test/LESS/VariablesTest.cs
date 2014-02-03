using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test.Less
{
    [TestClass]
    public class VariablesTest
    {
        private Sorter _sorter = new Sorter();

        [TestMethod, TestCategory("Variables")]
        public void Variables1()
        {
            string input = "div.item { @bg: blue; color: @bg}";
            string expected = "div.item {@bg: blue;color: @bg;}";

            string result = _sorter.SortLess(input);

            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("Variables")]
        public void Variables2()
        {
            string input = "div.item { color: @bg; @bg: blue; }";
            string expected = "div.item {@bg: blue;color: @bg;}";

            string result = _sorter.SortLess(input);

            Assert.AreEqual(expected, result);
        }
    }
}
