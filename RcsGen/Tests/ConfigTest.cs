namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;

    [TestClass]
    public class ConfigTest
    {
        string source = @"@inherits something 

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
        [TestMethod]
        public void TwoConfigNodes()
        {
            var node = Parser.Parse(source);
            Assert.AreEqual(2, node.Nodes.Count);
            var node1 = node.Nodes[0] as ConfigNode;
            var node2 = node.Nodes[1] as ConfigNode;

            Assert.IsNotNull(node1);
            Assert.AreEqual(NodeType.Config, node1.NodeType);
            Assert.AreEqual(ConfigCommand.Inherits, node1.ConfigCommand);
            Assert.AreEqual("something", node1.Parameters);

            Assert.IsNotNull(node2);
            Assert.AreEqual(NodeType.Config, node2.NodeType);
            Assert.AreEqual(ConfigCommand.Using, node2.ConfigCommand);
            Assert.AreEqual("somenamespace", node2.Parameters);
        }
    }
}
