using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CssSorter.Test
{
    [TestClass]
    public class CommentTest
    {
        private Sorter _sorter = new Sorter();

        [TestMethod, TestCategory("Comments")]
        public void SortDeclarations_InlineComment_StaysWithAttribute()
        {
            string[] input = new[] {
                "clear: none;",
                "float: none; /* inline comment 1 */"
            };

            string[] expected = new[]{                 
                "float: none; /* inline comment 1 */",
                "clear: none;"
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("", expected), string.Join("", result));
        }

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
                "\r\n/* border: none; */",
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
                "position: relative;",                
                "top: 10px;",         
                "\r\n/* some comment */", 
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
                "position: relative;",                
                "top: 10px;",         
                "\r\n/* some comment */", 
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
                "position: relative;",                
                "top: 10px;",                
                "\r\n/* some comment */",
                "\r\n/* some comment2 */",
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
                "\r\n/* some comment3 */", 
                "\r\n/* some comment */",
            };

            string[] expected = new[]{
                "position: relative;",                
                "top: 10px;",    
                "\r\n/* some comment */",
                "\r\n/* some comment2 */", 
                "\r\n/* some comment3 */",                                             
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
                "position: relative;",                
                "top: 10px;",    
                "\r\n/* some comment */",
                "\r\n/* some comment2 */", 
                "\r\n/* some comment3\r\n" + 
                "some comment3 continued\r\n" +
                "some comment3 continued again*/",                         
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
                "position: relative;",                
                "top: 10px;",    
                "\r\n/* some comment */",
                "\r\n/* some comment2 */", 
                "\r\n/* some comment3\r\n" +
                "some comment3 continued*/",                             
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("\n", expected), string.Join("\n", result));
        }

        [TestMethod, TestCategory("Comments")]
        public void Comment8()
        {
            string[] input = new[] { 
                "float: left;",
                "margin-top: 25px;",       
                "margin-left: 50px;",
                "margin-bottom: 10px;",
                "/*clear: both;", 
                "float: left;*/", 
            };

            string[] expected = new[]{
                "float: left;",
                "margin-top: 25px;", 
                "margin-bottom: 10px;",
                "margin-left: 50px;",                
                "\r\n/*clear: both;\r\n" +
                "float: left;*/",                            
            };

            string[] result = _sorter.SortDeclarations(input);

            Assert.AreEqual(string.Join("\n", expected), string.Join("\n", result));
        }
    }
}

