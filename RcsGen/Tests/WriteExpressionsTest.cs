namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;

    [TestClass]
    public class WriteExpressionsTest
    {
        string source1 = "@(hail)king";
        string source2 = "someFunc(@someArg(sayHello)).hello";
        string source3 = @"@hail
";
        string source4 = "@hail queen";

        [TestMethod]
        public void ExplicitWriteExpression()
        {
            var doc = Parser.Parse(source1);
            
            Assert.AreEqual(2, doc.Nodes.Nodes.Count);
            TestWriteNode(doc.Nodes.Nodes[0] as ContentNode);

            var node = doc.Nodes.Nodes[1] as ContentNode;
            Assert.IsNotNull(node);
            Assert.AreEqual(NodeType.Literal, node.NodeType);
            Assert.AreEqual("king", node.Content);
        }

        [TestMethod]
        public void SimpleWriteExpression()
        {
            var doc = Parser.Parse(source3);

            Assert.AreEqual(2, doc.Nodes.Nodes.Count);
            TestWriteNode(doc.Nodes.Nodes[0] as ContentNode);

            Assert.AreEqual(NodeType.Eol, doc.Nodes.Nodes[1].NodeType);
        }

        [TestMethod]
        public void SimpleWriteExpressionInParentheses()
        {
            var doc = Parser.Parse(source2);

            Assert.AreEqual(3, doc.Nodes.Nodes.Count);
            var node = (ContentNode)doc.Nodes.Nodes[0];
            Assert.AreEqual(NodeType.Literal, node.NodeType);
            Assert.AreEqual("someFunc(", node.Content);

            node = (ContentNode)doc.Nodes.Nodes[1];
            Assert.AreEqual(NodeType.WriteExpression, node.NodeType);
            Assert.AreEqual("someArg(sayHello)", node.Content);

            node = (ContentNode)doc.Nodes.Nodes[2];
            Assert.AreEqual(NodeType.Literal, node.NodeType);
            Assert.AreEqual(").hello", node.Content);
        }

        [TestMethod]
        public void SimpleWriteExpressionFollowedByText()
        {
            var doc = Parser.Parse(source4);

            Assert.AreEqual(2, doc.Nodes.Nodes.Count);
            TestWriteNode(doc.Nodes.Nodes[0] as ContentNode);

            var node = doc.Nodes.Nodes[1] as ContentNode;
            Assert.IsNotNull(node);
            Assert.AreEqual(NodeType.Literal, node.NodeType);
            Assert.AreEqual(" queen", node.Content);
        }

        private void TestWriteNode(ContentNode node)
        {
            Assert.IsNotNull(node);
            Assert.AreEqual(NodeType.WriteExpression, node.NodeType);
            Assert.AreEqual("hail", node.Content);
        }
    }
}
