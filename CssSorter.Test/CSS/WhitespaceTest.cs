using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test
{
    [TestClass]
    public class WhitespaceTest
    {
        private Sorter _sorter = new Sorter();

        [TestMethod, TestCategory("Whitespace")]
        public void Whitespace1()
        {
            string[] input = new[] {
                "top: 10px;",
                "   ",
                "position: relative;"
            };

            string[] expected = new[]{
                "position: relative;",
                "top: 10px;",
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }

        [TestMethod, TestCategory("Whitespace")]
        public void Whitespace2()
        {
            string[] input = new[] {
                "top: 10px;",
                "\t\t\n\r",
                "position: relative;"
            };

            string[] expected = new[]{            
                "position: relative;",
                "top: 10px;",
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }
    }
}
