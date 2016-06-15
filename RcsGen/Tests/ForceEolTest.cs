namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;

    [TestClass]
    public class ForceEolTest
    {
        string source = @"sometext
@newline
someothertext";
        [TestMethod]
        public void ForceEol()
        {
            var doc = Parser.Parse(source);

            Assert.AreEqual(4, doc.Nodes.Nodes.Count);

            var node = (ContentNode)doc.Nodes.Nodes[0];
            Assert.AreEqual(NodeType.Literal, node.NodeType);
            Assert.AreEqual("sometext", node.Content);

            Assert.AreEqual(NodeType.Eol, doc.Nodes.Nodes[1].NodeType);
            Assert.AreEqual(NodeType.ForceEol, doc.Nodes.Nodes[2].NodeType);

            node = (ContentNode)doc.Nodes.Nodes[3];
            Assert.AreEqual(NodeType.Literal, node.NodeType);
            Assert.AreEqual("someothertext", node.Content);
        }
    }
}
