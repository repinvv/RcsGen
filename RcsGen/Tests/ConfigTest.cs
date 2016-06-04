namespace RcsGen.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    [TestClass]
    public class ConfigTest
    {
        string inherits = @"@inherits SomeClass
";
        string inheritsWithParams = @"@inherits SomeClass(MyClass myClass, OtherClass otherClass)
";
        string inheritsWithParams2 = @"@inherits SomeClass (MyClass myClass, OtherClass otherClass)
";

        [TestMethod]
        public void SimpleInherits()
        {
            var doc = Parser.Parse(inherits);

            Assert.AreEqual(1, doc.Nodes.Count);
            var node = (InheritsNode)doc.Nodes[0];
            Assert.AreEqual(NodeType.Config, node.NodeType);
            Assert.AreEqual(ConfigCommand.Inherits, node.ConfigCommand);
            Assert.AreEqual("SomeClass", node.BaseClass);
            Assert.AreEqual(0, node.ConstructorParameters.Count);
        }

        [TestMethod]
        public void ParametrizedInherits()
        {
            var doc = Parser.Parse(inheritsWithParams);
            Assert.AreEqual(1, doc.Nodes.Count);
            TestParametrizedInherits((InheritsNode)doc.Nodes[0]);
        }

        [TestMethod]
        public void ParametrizedInheritsSpaced()
        {
            var doc = Parser.Parse(inheritsWithParams2);
            Assert.AreEqual(1, doc.Nodes.Count);
            TestParametrizedInherits((InheritsNode)doc.Nodes[0]);
        }

        private void TestParametrizedInherits(InheritsNode node)
        {
            Assert.AreEqual(NodeType.Config, node.NodeType);
            Assert.AreEqual(ConfigCommand.Inherits, node.ConfigCommand);
            Assert.AreEqual("SomeClass", node.BaseClass);
            Assert.AreEqual(2, node.ConstructorParameters.Count);

            Assert.AreEqual("MyClass", node.ConstructorParameters[0].Item1);
            Assert.AreEqual("myClass", node.ConstructorParameters[0].Item2);
            Assert.AreEqual("OtherClass", node.ConstructorParameters[1].Item1);
            Assert.AreEqual("otherClass", node.ConstructorParameters[1].Item2);
        }
    }
}
