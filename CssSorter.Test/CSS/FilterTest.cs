using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test
{
    [TestClass]
    public class FilterTest
    {
        private Sorter _sorter = new Sorter();

        [TestMethod, TestCategory("Filter")]
        public void Filter1()
        {
            string[] input = new[] {
                "*background: blue;",
                "background: blue;",
                "filter: 10px;",
            };

            string[] expected = new[]{                            
                "background: blue;",
                "*background: blue;",
                "filter: 10px;",
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }
    }
}
