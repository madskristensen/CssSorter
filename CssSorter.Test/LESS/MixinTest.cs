using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test.Less
{
    [TestClass]
    public class MixinTest
    {
        private Sorter _sorter = new Sorter();

        [TestMethod, TestCategory("Mixins")]
        public void Mixin1()
        {
            string input = "div.item { top: 10px; position: relative; .mixin(); }";
            string expected = "div.item {position: relative;top: 10px;.mixin();}";

            string result = _sorter.SortLess(input);

            Assert.AreEqual(expected, result);
        }

        [TestMethod, TestCategory("Mixins")]
        public void Mixin2()
        {
            string input = "div.item { top: 10px; position: relative; .mixin(@red, 5px); display: block; }";
            string expected = "div.item {position: relative;top: 10px;display: block;.mixin(@red, 5px);}";

            string result = _sorter.SortLess(input);

            Assert.AreEqual(expected, result);
        }
    }
}
