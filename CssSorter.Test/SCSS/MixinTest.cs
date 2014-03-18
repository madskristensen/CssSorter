using CssSorter.Test.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test.Scss
{
    [TestClass]
    public class MixinTest
    {
        private Sorter _sorter = new Sorter();

        [HostType("VS IDE")]
        [TestMethod, TestCategory("Mixins")]
        public void Mixin1()
        {
            VSHost.ReadyingSolution();

            string input = "div.item { top: 10px; position: relative; .mixin(); }";
            string expected = "div.item {position: relative;top: 10px;.mixin();}";

            string result = _sorter.SortScss(input);

            Assert.AreEqual(expected, result);
        }

        [HostType("VS IDE")]
        [TestMethod, TestCategory("Mixins")]
        public void Mixin2()
        {
            VSHost.ReadyingSolution();

            string input = "div.item { top: 10px; position: relative; .mixin(@red, 5px); display: block; }";
            string expected = "div.item {position: relative;top: 10px;display: block;.mixin(@red, 5px);}";

            string result = _sorter.SortScss(input);

            Assert.AreEqual(expected, result);
        }
    }
}
