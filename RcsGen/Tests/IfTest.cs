namespace RcsGen.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;

    [TestClass]
    public class IfTest
    {
        string source = "ha@if(smth){ifSmth}ha";
        string source2 = "ha@if(smth){ifSmth}else{smthElse}ha";
        string source3 = @"ha@if(smth){
    ifSmth}
    }ha";
        string source4 = @"ha@if(smth){
    ifSmth  }
    }
    else
    {
    smthElse }
    }ha";
        [TestMethod]
        public void OneLineIf()
        {
            var node = Parser.Parse(source);
            TestFirstAndLastNodes(node.Nodes.Nodes);
            var ifNode = (IfNode)node.Nodes.Nodes[1];
            Assert.AreEqual(1, ifNode.IfNodes.Nodes.Count);
            Assert.AreEqual(0, ifNode.ElseNodes.Nodes.Count);
            Assert.AreEqual(NodeType.Literal, ifNode.IfNodes.Nodes[0].NodeType);
            Assert.AreEqual("ifSmth", ((ContentNode)ifNode.IfNodes.Nodes[0]).Content);
        }

        [TestMethod]
        public void OneLineIfElse()
        {
            var node = Parser.Parse(source2);
            TestFirstAndLastNodes(node.Nodes.Nodes);
            var ifNode = (IfNode)node.Nodes.Nodes[1];
            Assert.AreEqual(1, ifNode.IfNodes.Nodes.Count);
            Assert.AreEqual(1, ifNode.ElseNodes.Nodes.Count);
            Assert.AreEqual(NodeType.Literal, ifNode.IfNodes.Nodes[0].NodeType);
            Assert.AreEqual("ifSmth", ((ContentNode)ifNode.IfNodes.Nodes[0]).Content);

            Assert.AreEqual(NodeType.Literal, ifNode.ElseNodes.Nodes[0].NodeType);
            Assert.AreEqual("smthElse", ((ContentNode)ifNode.ElseNodes.Nodes[0]).Content);
        }

        [TestMethod]
        public void MultiLineIf()
        {
            var node = Parser.Parse(source3);
            TestFirstAndLastNodes(node.Nodes.Nodes);
            var ifNode = (IfNode)node.Nodes.Nodes[1];
            Assert.AreEqual(2, ifNode.IfNodes.Nodes.Count);
            Assert.AreEqual(0, ifNode.ElseNodes.Nodes.Count);
            Assert.AreEqual(NodeType.Literal, ifNode.IfNodes.Nodes[0].NodeType);
            Assert.AreEqual("    ifSmth}", ((ContentNode)ifNode.IfNodes.Nodes[0]).Content);
            Assert.AreEqual(NodeType.Eol, ifNode.IfNodes.Nodes[1].NodeType);
        }

        [TestMethod]
        public void MultiLineIfElse()
        {
            var node = Parser.Parse(source4);
            TestFirstAndLastNodes(node.Nodes.Nodes);
            var ifNode = (IfNode)node.Nodes.Nodes[1];
            Assert.AreEqual(2, ifNode.IfNodes.Nodes.Count);
            Assert.AreEqual(2, ifNode.ElseNodes.Nodes.Count);
            Assert.AreEqual(NodeType.Literal, ifNode.IfNodes.Nodes[0].NodeType);
            Assert.AreEqual("    ifSmth  }", ((ContentNode)ifNode.IfNodes.Nodes[0]).Content);
            Assert.AreEqual(NodeType.Eol, ifNode.IfNodes.Nodes[1].NodeType);

            Assert.AreEqual(NodeType.Literal, ifNode.ElseNodes.Nodes[0].NodeType);
            Assert.AreEqual("    smthElse }", ((ContentNode)ifNode.ElseNodes.Nodes[0]).Content);
            Assert.AreEqual(NodeType.Eol, ifNode.ElseNodes.Nodes[1].NodeType);
        }

        private void TestFirstAndLastNodes(IReadOnlyList<Node> nodes)
        {
            Assert.AreEqual(3, nodes.Count);
            TestLiteralNode(nodes[0]);
            TestLiteralNode(nodes[2]);
        }

        private void TestLiteralNode(Node node)
        {
            Assert.AreEqual(NodeType.Literal, node.NodeType);
            Assert.AreEqual("ha", ((ContentNode)node).Content);
        }
    }
}
