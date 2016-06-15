namespace RcsGen.Tests
{
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
        string unclosed = @"@for(smth){
    smthMore}
    @if(smthelse(){234}
}
";
        string unclosed2 = @"@for(smth){
    smthMore}
    @if(smthelse()){234

";

        [TestMethod]
        public void EnclosedOneLineFor()
        {
            var node = Parser.Parse(source);
            Assert.AreEqual(1, node.Nodes.Nodes.Count);
            var forNode = (ForNode)node.Nodes.Nodes[0];
            TestForNode(forNode);
            TestForNodeChild(forNode);
        }

        [TestMethod]
        public void SpacedOneLineFor()
        {
            var node = Parser.Parse(source4);
            Assert.AreEqual(1, node.Nodes.Nodes.Count);
            var forNode = (ForNode)node.Nodes.Nodes[0];
            TestForNode(forNode);
            TestForNodeChild(forNode);
        }

        [TestMethod]
        public void EnclosedForWithEgyptBraces()
        {
            var node = Parser.Parse(source2);
            Assert.AreEqual(1, node.Nodes.Nodes.Count);
            var forNode = (ForNode)node.Nodes.Nodes[0];
            TestForNode(forNode);
            TestForNodeMultiLineChildren(forNode);
        }

        [TestMethod]
        public void EnclosedForWithSharpBraces()
        {
            var node = Parser.Parse(source3);
            Assert.AreEqual(1, node.Nodes.Nodes.Count);
            var forNode = (ForNode)node.Nodes.Nodes[0];
            TestForNode(forNode);
            TestForNodeMultiLineChildren(forNode);
        }

        [TestMethod]
        public void SpacedForWithEgyptBraces()
        {
            var node = Parser.Parse(source5);
            Assert.AreEqual(1, node.Nodes.Nodes.Count);
            var forNode = (ForNode)node.Nodes.Nodes[0];
            TestForNode(forNode);
            TestForNodeMultiLineChildren(forNode);
        }

        private void TestForNodeMultiLineChildren(ForNode forNode)
        {
            Assert.AreEqual(2, forNode.ChildNodes.Nodes.Count);
            var childNode = (ContentNode)forNode.ChildNodes.Nodes[0];
            Assert.AreEqual(NodeType.Literal, childNode.NodeType);
            Assert.AreEqual("    smthMore}", childNode.Content);
            Assert.AreEqual(NodeType.Eol, forNode.ChildNodes.Nodes[1].NodeType);
        }

        private static void TestForNodeChild(ForNode forNode)
        {
            Assert.AreEqual(1, forNode.ChildNodes.Nodes.Count);
            var childNode = (ContentNode)forNode.ChildNodes.Nodes[0];
            Assert.AreEqual(NodeType.Literal, childNode.NodeType);
            Assert.AreEqual("smthMore", childNode.Content);
        }

        private static void TestForNode(ForNode forNode)
        {
            Assert.AreEqual(NodeType.For, forNode.NodeType);
            Assert.AreEqual("for", forNode.Keyword);
            Assert.AreEqual("smth", forNode.Condition);
        }

        [TestMethod]
        public void UnclosedIfCondition()
        {
            var node = Parser.Parse(unclosed);
            Assert.AreEqual(1, node.Nodes.Nodes.Count);
            var forNode = (ForNode)node.Nodes.Nodes[0];
            TestForNode(forNode);

            var childNode = (ContentNode)forNode.ChildNodes.Nodes[0];
            Assert.AreEqual(NodeType.Literal, childNode.NodeType);
            Assert.AreEqual("    smthMore}", childNode.Content);
            Assert.AreEqual(NodeType.Eol, forNode.ChildNodes.Nodes[1].NodeType);
        }

        [TestMethod]
        public void UnclosedIfContent()
        {
            var node = Parser.Parse(unclosed2);
            Assert.AreEqual(1, node.Nodes.Nodes.Count);
            var forNode = (ForNode)node.Nodes.Nodes[0];
            TestForNode(forNode);

            var childNode = (ContentNode)forNode.ChildNodes.Nodes[0];
            Assert.AreEqual(NodeType.Literal, childNode.NodeType);
            Assert.AreEqual("    smthMore}", childNode.Content);
            Assert.AreEqual(NodeType.Eol, forNode.ChildNodes.Nodes[1].NodeType);
        }
    }
}
