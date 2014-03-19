using CssSorter.Test.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test.Scss
{
    [TestClass]
    public class NestingTest
    {
        private Sorter _sorter = new Sorter();

        [HostType("VS IDE")]
        [TestMethod, TestCategory("Nesting")]
        public void Nesting1()
        {
            VSHost.ReadyingSolution();

            string input = "div.item { top: 10px; position: relative; div{color:red; top: 5px;}}";
            string expected = "div.item {position: relative;top: 10px; div{top: 5px;color:red;}}";

            string result = _sorter.SortScss(input);

            Assert.AreEqual(expected, result);
        }
    }
}
