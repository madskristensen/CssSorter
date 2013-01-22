using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test
{
    [TestClass]
    public class VendorTest
    {
        private Sorter _sorter = new Sorter();

        [TestMethod, TestCategory("Vendor")]
        public void Vendor1()
        {
            string[] input = new[] {
                "box-shadow: 10px;",
                "-webkit-box-shadow: 10px;",
                "-moz-box-shadow: 10px;",
            };

            string[] expected = new[]{            
                "-moz-box-shadow: 10px;",
                "-webkit-box-shadow: 10px;",
                "box-shadow: 10px;",
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }

        [TestMethod, TestCategory("Vendor")]
        public void Vendor2()
        {
            string[] input = new[] {
                "-webkit-text-shadow: 10px;",
                "text-shadow: 10px;",
                "-ms-text-shadow: 10px;",
                "box-shadow: 10px;",
                "-webkit-box-shadow: 10px;",
               "-moz-box-shadow: 10px;",                
            };

            string[] expected = new[]{            
                "-moz-box-shadow: 10px;",
                "-webkit-box-shadow: 10px;",
                "box-shadow: 10px;",
                "-ms-text-shadow: 10px;",
                "-webkit-text-shadow: 10px;",
                "text-shadow: 10px;",                
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }

        [TestMethod, TestCategory("Vendor")]
        public void Vendor3()
        {
            string[] input = new[] {
                "background: 10px;",
                "-shadow: 10px;",
            };

            string[] expected = new[]{            
                "-shadow: 10px;",
                "background: 10px;",                
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }

        [TestMethod, TestCategory("Vendor")]
        public void VendorValues()
        {
            string[] input = new[] {
                "white-space: pre-wrap;",
                "white-space: -moz-pre-wrap;",
                "white-space: -pre-wrap;",
                "white-space: -o-pre-wrap;",
            };

            string[] expected = new[]{                                            
                "white-space: -moz-pre-wrap;",
                "white-space: -o-pre-wrap;",
                "white-space: -pre-wrap;",                
                "white-space: pre-wrap;",
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }
    }
}
