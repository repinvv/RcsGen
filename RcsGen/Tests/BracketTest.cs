namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;

    [TestClass]
    public class BracketTest
    {
        string sourceSquare = @"return @keyfields[0].Name ==";

        [TestMethod]
        public void SquaredBracketInImplicitExpression()
        {
            var doc = Parser.Parse(sourceSquare);
            Assert.AreEqual(3, doc.Nodes.Nodes.Count);
            var node0 = (ContentNode)doc.Nodes.Nodes[0];
            Assert.AreEqual(NodeType.Literal, node0.NodeType);
            Assert.AreEqual("return ", node0.Content);

            var node1 = (ContentNode)doc.Nodes.Nodes[1];
            Assert.AreEqual(NodeType.WriteExpression, node1.NodeType);
            Assert.AreEqual("keyfields[0].Name", node1.Content);

            var node2 = (ContentNode)doc.Nodes.Nodes[2];
            Assert.AreEqual(NodeType.Literal, node2.NodeType);
            Assert.AreEqual(" ==", node2.Content);
        }
    }
}
