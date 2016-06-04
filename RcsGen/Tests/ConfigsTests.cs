namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    [TestClass]
    public class ConfigsTests
    {
        string implements = @"@implements SomeInterface SomeInterface2
";

        string usings = @"@using Some.Namespace SomeName.Space
";

        string visibility = @"@visibility internal
";

        string member = @"@member{public string FileName { get; set; }}
";

        [TestMethod]
        public void MemberTest()
        {
            var memberContent = "public string FileName { get; set; }";
            var doc = Parser.Parse(member);
            Assert.AreEqual(1, doc.Nodes.Count);
            var node = (MemberNode)doc.Nodes[0];
            Assert.AreEqual(ConfigCommand.Member, node.ConfigCommand);
            Assert.AreEqual(memberContent, node.Member);
        }

        [TestMethod]
        public void VisibilityTest()
        {
            var doc = Parser.Parse(visibility);
            Assert.AreEqual(1, doc.Nodes.Count);
            Assert.AreEqual(NodeType.Config, doc.Nodes[0].NodeType);
            var node = (VisibilityNode)doc.Nodes[0];
            Assert.AreEqual(ConfigCommand.Visibility, node.ConfigCommand);
            Assert.AreEqual("internal", node.Visibility);
        }

        [TestMethod]
        public void ImplementsTest()
        {
            var ifaces = new[] { "SomeInterface", "SomeInterface2" };
            var doc = Parser.Parse(implements);
            Assert.AreEqual(1, doc.Nodes.Count);
            Assert.AreEqual(NodeType.Config, doc.Nodes[0].NodeType);
            var node = (ImplementsNode)doc.Nodes[0];
            Assert.AreEqual(ConfigCommand.Implements, node.ConfigCommand);
            CollectionAssert.AreEquivalent(ifaces, node.Interfaces);
        }

        [TestMethod]
        public void UsingTest()
        {
            var namespaces = new[] { "Some.Namespace", "SomeName.Space" };
            var doc = Parser.Parse(usings);
            Assert.AreEqual(1, doc.Nodes.Count);
            Assert.AreEqual(NodeType.Config, doc.Nodes[0].NodeType);
            var node = (UsingNode)doc.Nodes[0];
            Assert.AreEqual(ConfigCommand.Using, node.ConfigCommand);
            CollectionAssert.AreEquivalent(namespaces, node.Usings);
        }

    }
}
