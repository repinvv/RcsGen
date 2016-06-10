namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;

    [TestClass]
    public class CommentTest
    {
        string source = @"@* 423423
asdfsfadsfad
asdfsadf
asf
sdafsadf
@
234
@inherits *@
";
        [TestMethod]
        public void CommentedTextShouldNotProduceAnything()
        {
            var node = Parser.Parse(source);
            Assert.AreEqual(0, node.Nodes.Nodes.Count);
        }
    }
}
