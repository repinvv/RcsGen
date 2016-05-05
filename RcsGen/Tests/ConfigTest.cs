namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;

    [TestClass]
    public class ConfigTest
    {
        string source2configs = @"@inherits something 

@*asdfsfadsfad
asdfsadf
asf
sdafsadf
@
234
@inherits all*@
@using somenamespace
@using 
";

        string sourceconfigliterals = @" 
@inherits something 
234234
@using somenamespace 
";

        [TestMethod]
        public void TwoConfigNodes()
        {
            var node = Parser.Parse(source2configs);
            Assert.AreEqual(2, node.Nodes.Count);
            var node1 = node.Nodes[0] as ConfigNode;
            var node2 = node.Nodes[1] as ConfigNode;
            
            TestConfigNode1(node1);
            Assert.IsNotNull(node2);
            Assert.AreEqual(NodeType.Config, node2.NodeType);
            Assert.AreEqual(ConfigCommand.Using, node2.ConfigCommand);
            Assert.AreEqual("somenamespace", node2.Parameters);
        }

        [TestMethod]
        public void ConfigNodeIsNotRecognizedAfterLiterals()
        {
            var node = Parser.Parse(sourceconfigliterals);
            Assert.AreEqual(5, node.Nodes.Count);
            var node1 = node.Nodes[0] as ConfigNode;
            TestConfigNode1(node1);
            var node2 = node.Nodes[1] as ContentNode;
            Assert.IsNotNull(node2);
            Assert.AreEqual(NodeType.Literal, node2.NodeType);
            Assert.AreEqual("234234", node2.Content);

            Assert.AreEqual(NodeType.Eol, node.Nodes[2].NodeType);

            Assert.AreEqual(NodeType.WriteExpression, node.Nodes[3].NodeType);

            var node3 = node.Nodes[4] as ContentNode;
            Assert.IsNotNull(node3);
            Assert.AreEqual(NodeType.Literal, node3.NodeType);
            Assert.AreEqual("somenamespace ", node3.Content);
            
            
            Assert.AreEqual(NodeType.Eol, node.Nodes[5].NodeType);
        }

        private void TestConfigNode1(ConfigNode node1)
        {
            Assert.IsNotNull(node1);
            Assert.AreEqual(NodeType.Config, node1.NodeType);
            Assert.AreEqual(ConfigCommand.Inherits, node1.ConfigCommand);
            Assert.AreEqual("something", node1.Parameters);
        }
    }
}
