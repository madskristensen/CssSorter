using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test
{
    [TestClass]
    public class HackTest
    {
        private Sorter _sorter = new Sorter();

        [TestMethod, TestCategory("Hacks")]
        public void Hack1()
        {
            string[] input = new[] {
                "top: 10px;",
                "_position: relative;"
            };

            string[] expected = new[]{            
                "_position: relative;",
                "top: 10px;",
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }

        [TestMethod, TestCategory("Hacks")]
        public void Hack2()
        {
            string[] input = new[] {
               "width: 24px;",
               "*height: 24px;",
               "display: block;",
               "_float: left;",
               "margin-right: 6px;",
               "background: url(../images/mini-networks-sprite3.png) 0 0 no-repeat;"
            };

            string[] expected = new[]{            
               "display: block;",
               "_float: left;",
               "margin-right: 6px;",
               "width: 24px;",
               "*height: 24px;",               
               "background: url(../images/mini-networks-sprite3.png) 0 0 no-repeat;"
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }

        [TestMethod, TestCategory("Hacks")]
        public void Hack3()
        {
            string[] input = new[] {
                "_position: relative;",
                "position: aboslute;",                
            };

            string[] expected = new[]{                            
                "position: aboslute;",
                "_position: relative;",
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }
    }
}
