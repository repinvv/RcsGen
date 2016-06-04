namespace RcsGen.Generation
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal class Config
    {
        public List<string> Usings { get; set; }

        public List<string> Interfaces { get; set; }

        public List<string> Members { get; set; }

        public InheritsNode InheritsNode { get; set; }

        public ConstructorParametersNode ConstructorParametersNode { get; set; }

        public string Visibility { get; set; }
    }
}
