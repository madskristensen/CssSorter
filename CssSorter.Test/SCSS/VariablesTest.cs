using CssSorter.Test.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test.Scss
{
    [TestClass]
    public class VariablesTest
    {
        private Sorter _sorter = new Sorter();

        [HostType("VS IDE")]
        [TestMethod, TestCategory("Variables")]
        public void Variables1()
        {
            VSHost.ReadyingSolution();

            string input = "div.item { $bg: blue; color: $bg}";
            string expected = "div.item {$bg: blue;color: $bg;}";

            string result = _sorter.SortScss(input);

            Assert.AreEqual(expected, result);
        }

        [HostType("VS IDE")]
        [TestMethod, TestCategory("Variables")]
        public void Variables2()
        {
            VSHost.ReadyingSolution();

            string input = "div.item { color: $bg; $bg: blue; }";
            string expected = "div.item {$bg: blue;color: $bg;}";

            string result = _sorter.SortScss(input);

            Assert.AreEqual(expected, result);
        }
    }
}
