namespace RcsGen.SyntaxTree.Nodes.ConfigNodes
{
    using System;
    using System.Collections.Generic;

    internal class InheritsNode : ConfigNode
    {
        public InheritsNode(string baseClass, List<Tuple<string, string>> constructorParameters)
            : base(ConfigCommand.Inherits)
        {
            BaseClass = baseClass;
            ConstructorParameters = constructorParameters;
        }

        public string BaseClass { get; }

        public List<Tuple<string, string>> ConstructorParameters { get; }
    }
}
