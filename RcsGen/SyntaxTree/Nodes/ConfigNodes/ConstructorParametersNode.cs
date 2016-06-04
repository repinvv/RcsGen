namespace RcsGen.SyntaxTree.Nodes.ConfigNodes
{
    using System;
    using System.Collections.Generic;

    internal class ConstructorParametersNode : ConfigNode
    {
        public ConstructorParametersNode(List<Tuple<string, string>> constructorParameters) 
            : base(ConfigCommand.ConstructorParameters)
        {
            ConstructorParameters = constructorParameters;
        }

        public List<Tuple<string, string>> ConstructorParameters { get; }
    }
}
