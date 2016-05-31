namespace RcsGen.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;

    [TestClass]
    public class ForTest
    {
        string source = "@for(smth){smthMore}";
        string source2 = @"@for(smth){
    smthMore}
    }";
        string source3 = @"@for(smth)
        {
        smthMore}
        }";

        string source4 = "@for (smth) {smthMore}";
        string source5 = @"@for (smth) {
    smthMore}
    }";
        string source6 = @"@for (smth)
        {
        smthMore}
        }";

        [TestMethod]
        public void EnclosedOneLineFor()
        {
            var node = Parser.Parse(source);
            Assert.AreEqual(2, node.Nodes.Count);
            var forNode = (ForNode)node.Nodes[0];
            TestForNode(forNode);
            TestForNodeChild(forNode);
            Assert.AreEqual(NodeType.Eol, node.Nodes[1].NodeType);
        }

        [TestMethod]
        public void SpacedOneLineFor()
        {
            var node = Parser.Parse(source4);
            Assert.AreEqual(2, node.Nodes.Count);
            var forNode = (ForNode)node.Nodes[0];
            TestForNode(forNode);
            TestForNodeChild(forNode);
            Assert.AreEqual(NodeType.Eol, node.Nodes[1].NodeType);
        }

        private static void TestForNodeChild(ForNode forNode)
        {
            Assert.AreEqual(1, forNode.ChildNodes.Count);
            var childNode = (ContentNode)forNode.ChildNodes[0];
            Assert.AreEqual(NodeType.Literal, childNode.NodeType);
            Assert.AreEqual("smthMore", childNode.Content);
        }

        private static void TestForNode(ForNode forNode)
        {
            Assert.AreEqual(NodeType.For, forNode.NodeType);
            Assert.AreEqual("for", forNode.Keyword);
            Assert.AreEqual("smth", forNode.Condition);
        }
    }
}
