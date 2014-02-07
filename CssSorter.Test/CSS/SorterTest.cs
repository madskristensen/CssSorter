using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test
{
    [TestClass]
    public class SorterTest
    {
        private Sorter _sorter = new Sorter();

        [TestMethod, TestCategory("Sorter")]
        public void Sorter1()
        {
            string[] input = new[] {
                "top: 10px;",
                "position: relative;"
            };

            string[] expected = new[]{            
                "position: relative;",
                "top: 10px;",
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }

        [TestMethod, TestCategory("Sorter")]
        public void Sorter2()
        {
            string[] input = new[] {
               "width: 24px;",
               "height: 24px;",
               "display: block;",
               "float: left;",
               "margin-right: 6px;",
               "background: url(../images/mini-networks-sprite3.png) 0 0 no-repeat;"
            };

            string[] expected = new[]{            
               "display: block;",
               "float: left;",
               "margin-right: 6px;",
               "width: 24px;",
               "height: 24px;",               
               "background: url(../images/mini-networks-sprite3.png) 0 0 no-repeat;"
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }

        [TestMethod, TestCategory("Sorter")]
        public void SorterMissingSemicolon()
        {
            string[] input = new[] {
                "top: 10px;",
                "position: relative"
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
