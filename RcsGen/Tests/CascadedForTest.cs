namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;

    [TestClass]
    public class CascadedForTest
    {
        string source = "@for(smth1){@foreach(smth2){@if(smth3){inside}}}";
        string source2 = @"@for(smth1){
@foreach(smth2){
@if(smth3){
        inside
        }
    }
}";
        [TestMethod]
        public void CascadedOneLineForAndIf()
        {
            var node = Parser.Parse(source);
            Assert.AreEqual(1, node.Nodes.Count);

            var forNode = (ForNode)node.Nodes[0];
            TestRootForNode(forNode);
            Assert.AreEqual(1, forNode.ChildNodes.Count);

            forNode = (ForNode)forNode.ChildNodes[0];
            TestChildForNode(forNode);
            Assert.AreEqual(1, forNode.ChildNodes.Count);

            var ifNode = (IfNode)forNode.ChildNodes[0];
            TestIfNode(ifNode);
            Assert.AreEqual(1, ifNode.IfNodes.Count);
            Assert.AreEqual(0, ifNode.ElseNodes.Count);
            Assert.AreEqual(NodeType.Literal, ifNode.IfNodes[0].NodeType);
            Assert.AreEqual("inside", ((ContentNode)ifNode.IfNodes[0]).Content);
        }

        [TestMethod]
        public void CascadedMultiLineForAndIf()
        {
            var node = Parser.Parse(source2);
            Assert.AreEqual(1, node.Nodes.Count);

            var forNode = (ForNode)node.Nodes[0];
            TestRootForNode(forNode);
            Assert.AreEqual(2, forNode.ChildNodes.Count);
            Assert.AreEqual(NodeType.Eol, forNode.ChildNodes[1].NodeType);

            forNode = (ForNode)forNode.ChildNodes[0];
            TestChildForNode(forNode);
            Assert.AreEqual(2, forNode.ChildNodes.Count);
            Assert.AreEqual(NodeType.Eol, forNode.ChildNodes[1].NodeType);

            var ifNode =(IfNode)forNode.ChildNodes[0];
            TestIfNode(ifNode);
            Assert.AreEqual(2, ifNode.IfNodes.Count);
            Assert.AreEqual(0, ifNode.ElseNodes.Count);
            Assert.AreEqual(NodeType.Eol, ifNode.IfNodes[1].NodeType);
            Assert.AreEqual(NodeType.Literal, ifNode.IfNodes[0].NodeType);
            Assert.AreEqual("        inside", ((ContentNode)ifNode.IfNodes[0]).Content);
        }

        private void TestIfNode(IfNode ifNode)
        {
            Assert.AreEqual(NodeType.If, ifNode.NodeType);
            Assert.AreEqual("smth3", ifNode.Condition);
        }

        private void TestRootForNode(ForNode forNode)
        {
            Assert.AreEqual(NodeType.For, forNode.NodeType);
            Assert.AreEqual("for", forNode.Keyword);
            Assert.AreEqual("smth1", forNode.Condition);
        }

        private void TestChildForNode(ForNode forNode)
        {
            Assert.AreEqual(NodeType.For, forNode.NodeType);
            Assert.AreEqual("foreach", forNode.Keyword);
            Assert.AreEqual("smth2", forNode.Condition);
        }
    }
}
