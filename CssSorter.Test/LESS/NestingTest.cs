using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test.Less
{
    [TestClass]
    public class NestingTest
    {
        private Sorter _sorter = new Sorter();

        [TestMethod, TestCategory("Nesting")]
        public void Nesting1()
        {
            string input = "div.item { top: 10px; position: relative; div{color:red; top: 5px;}}";
            string expected = "div.item {position: relative;top: 10px; div{top: 5px;color:red;}}";

            string result = _sorter.SortLess(input);

            Assert.AreEqual(expected, result);
        }
    }
}
