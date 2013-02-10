using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test
{
    [TestClass]
    public class CommentTest
    {
        private Sorter _sorter = new Sorter();


        [TestMethod, TestCategory("Comments")]
        public void Comment1()
        {
            string[] input = new[] {
                "top: 10px;",
                "/* border: none; */",
                "_position: relative;"
            };

            string[] expected = new[]{                 
                "_position: relative;",                
                "top: 10px;",          
                "/* border: none; */",
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }

        [TestMethod, TestCategory("Comments")]
        public void Comment2()
        {
            string[] input = new[] {
                "/* some comment */",
                "top: 10px;",                
                "position: relative;"
            };

            string[] expected = new[]{
                "/* some comment */", 
                "position: relative;",                
                "top: 10px;",                
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("\n", expected), string.Join("\n", result));
        }

        [TestMethod, TestCategory("Comments")]
        public void Comment3()
        {
            string[] input = new[] {                
                "top: 10px;",       
                "/* some comment */",
                "position: relative;"
            };

            string[] expected = new[]{
                "/* some comment */", 
                "position: relative;",                
                "top: 10px;",                
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("\n", expected), string.Join("\n", result));
        }

        [TestMethod, TestCategory("Comments")]
        public void Comment4()
        {
            string[] input = new[] {                
                "top: 10px;",       
                "/* some comment2 */",
                "position: relative;",
                "/* some comment */",
            };

            string[] expected = new[]{                 
                "/* some comment */",
                "/* some comment2 */",
                "position: relative;",                
                "top: 10px;",                
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("\n", expected), string.Join("\n", result));
        }

        [TestMethod, TestCategory("Comments")]
        public void Comment5()
        {
            string[] input = new[] {                
                "top: 10px;",       
                "/* some comment2 */",
                "position: relative;",
                "/* some comment3 */", 
                "/* some comment */",
            };

            string[] expected = new[]{
                "/* some comment */",
                "/* some comment2 */", 
                "/* some comment3 */",                 
                "position: relative;",                
                "top: 10px;",                
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("\n", expected), string.Join("\n", result));
        }

        [TestMethod, TestCategory("Comments")]
        public void Comment6()
        {
            string[] input = new[] { 
                "/* some comment */",
                "top: 10px;",       
                "/* some comment2 */",
                "position: relative;",
                "/* some comment3", 
                "some comment3 continued", 
                "some comment3 continued again*/", 
            };

            string[] expected = new[]{
                "/* some comment */",
                "/* some comment2 */", 
                "/* some comment3",
                "some comment3 continued", 
                "some comment3 continued again*/", 
                "position: relative;",                
                "top: 10px;",                
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("\n", expected), string.Join("\n", result));
        }

        [TestMethod, TestCategory("Comments")]
        public void Comment7()
        {
            string[] input = new[] { 
                "/* some comment */",
                "top: 10px;",       
                "/* some comment2 */",
                "position: relative;",
                "/* some comment3", 
                "some comment3 continued*/", 
            };

            string[] expected = new[]{
                "/* some comment */",
                "/* some comment2 */", 
                "/* some comment3",
                "some comment3 continued*/", 
                "position: relative;",                
                "top: 10px;",                
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("\n", expected), string.Join("\n", result));
        }
    }
}
