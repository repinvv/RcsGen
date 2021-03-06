﻿namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;

    [TestClass]
    public class WriteExpressionsComplexTest
    {
        string source1 = "@(aaa(bbb).ccc<ddd>(new eee(){fff = \" }ggg\\\" \", hhh = ' '}))king";

        [TestMethod]
        public void ComplexExplicitWriteExpression()
        {
            var doc = Parser.Parse(source1);

            Assert.AreEqual(2, doc.Nodes.Nodes.Count);
            var node = doc.Nodes.Nodes[0] as ContentNode;
            Assert.IsNotNull(node);
            Assert.AreEqual(NodeType.WriteExpression, node.NodeType);
            Assert.AreEqual("aaa(bbb).ccc<ddd>(new eee(){fff = \" }ggg\\\" \", hhh = ' '})", node.Content);

            node = doc.Nodes.Nodes[1] as ContentNode;
            Assert.IsNotNull(node);
            Assert.AreEqual(NodeType.Literal, node.NodeType);
            Assert.AreEqual("king", node.Content);
        }
    }
}
