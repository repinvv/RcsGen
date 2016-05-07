namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;

    [TestClass]
    public class WriteExpressionsTest
    {
        string source = @"@using namespace
@(hail)king
@hail queen
";
        [TestMethod]
        public void TwoTypesOfWriteExpression()
        {
            var node = Parser.Parse(source);
            
            Assert.AreEqual(7, node.Nodes.Count);
            Assert.AreEqual(NodeType.Config, node.Nodes[0].NodeType);
            
            TestWriteNode(node.Nodes[1] as ContentNode);
            Assert.AreEqual(NodeType.Eol, node.Nodes[3].NodeType);
            TestWriteNode(node.Nodes[4] as ContentNode);
            Assert.AreEqual(NodeType.Eol, node.Nodes[6].NodeType);

            var node1 = node.Nodes[5] as ContentNode;
            Assert.IsNotNull(node1);
            Assert.AreEqual(NodeType.Literal, node1.NodeType);
            Assert.AreEqual(" queen", node1.Content);

            var node2 = node.Nodes[2] as ContentNode;
            Assert.IsNotNull(node2);
            Assert.AreEqual(NodeType.Literal, node2.NodeType);
            Assert.AreEqual("king", node2.Content);
        }

        private void TestWriteNode(ContentNode node)
        {
            Assert.IsNotNull(node);
            Assert.AreEqual(NodeType.WriteExpression, node.NodeType);
            Assert.AreEqual("hail", node.Content);
        }
    }
}
