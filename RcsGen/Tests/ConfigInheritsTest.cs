namespace RcsGen.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    [TestClass]
    public class ConfigInheritsTest
    {
        string inherits = @"@inherits SomeClass
";
        string inheritsWithParams = @"@inherits SomeClass(MyClass myClass, OtherClass otherClass)
";
        string inheritsWithParams2 = @"@inherits SomeClass (MyClass myClass, OtherClass otherClass)
";
        string constructorParams = @"@constructor(MyClass myClass, OtherClass otherClass)
";
        string constructorParams2 = @"@constructor (MyClass myClass, OtherClass otherClass)
";

        [TestMethod]
        public void SimpleInherits()
        {
            var doc = Parser.Parse(inherits);

            Assert.AreEqual(1, doc.Nodes.Nodes.Count);
            var node = (InheritsNode)doc.Nodes.Nodes[0];
            Assert.AreEqual(NodeType.Config, node.NodeType);
            Assert.AreEqual(ConfigCommand.Inherits, node.ConfigCommand);
            Assert.AreEqual("SomeClass", node.BaseClass);
            Assert.AreEqual(0, node.ConstructorParameters.Count);
        }

        [TestMethod]
        public void ParametrizedInherits()
        {
            var doc = Parser.Parse(inheritsWithParams);
            Assert.AreEqual(1, doc.Nodes.Nodes.Count);
            TestParametrizedInherits((InheritsNode)doc.Nodes.Nodes[0]);
        }

        [TestMethod]
        public void ParametrizedInheritsSpaced()
        {
            var doc = Parser.Parse(inheritsWithParams2);
            Assert.AreEqual(1, doc.Nodes.Nodes.Count);
            TestParametrizedInherits((InheritsNode)doc.Nodes.Nodes[0]);
        }

        private void TestParametrizedInherits(InheritsNode node)
        {
            Assert.AreEqual(NodeType.Config, node.NodeType);
            Assert.AreEqual(ConfigCommand.Inherits, node.ConfigCommand);
            Assert.AreEqual("SomeClass", node.BaseClass);
            Assert.AreEqual(2, node.ConstructorParameters.Count);
            TestParams(node.ConstructorParameters);
        }

        private void TestConstructorNode(ConstructorParametersNode node)
        {
            Assert.AreEqual(NodeType.Config, node.NodeType);
            Assert.AreEqual(ConfigCommand.ConstructorParameters, node.ConfigCommand);
            Assert.AreEqual(2, node.ConstructorParameters.Count);
            TestParams(node.ConstructorParameters);
        }

        private void TestParams(List<Tuple<string, string>> parameters)
        {
            Assert.AreEqual("MyClass", parameters[0].Item1);
            Assert.AreEqual("myClass", parameters[0].Item2);
            Assert.AreEqual("OtherClass", parameters[1].Item1);
            Assert.AreEqual("otherClass", parameters[1].Item2);
        }
        
        [TestMethod]
        public void ConstructorParametersSimpleTest()
        {
            var doc = Parser.Parse(constructorParams);
            Assert.AreEqual(1, doc.Nodes.Nodes.Count);
            TestConstructorNode((ConstructorParametersNode)doc.Nodes.Nodes[0]);
        }

        [TestMethod]
        public void ConstructorParametersSpacedTest()
        {
            var doc = Parser.Parse(constructorParams2);
            Assert.AreEqual(1, doc.Nodes.Nodes.Count);
            TestConstructorNode((ConstructorParametersNode)doc.Nodes.Nodes[0]);
        }
    }
}
