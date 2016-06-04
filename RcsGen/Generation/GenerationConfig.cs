namespace RcsGen.Generation
{
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal static class GenerationConfig
    {
        public static Config GetGenerationConfig(Document document)
        {
            var configNodes = document.Nodes
                                      .Where(x => x.NodeType == NodeType.Config)
                                      .Cast<ConfigNode>()
                                      .ToList();
            var usings = configNodes
                .Where(x => x.ConfigCommand == ConfigCommand.Using)
                .SelectMany(x => ((UsingNode)x).Usings)
                .ToList();
            var inheritsNode = configNodes.LastOrDefault(x => x.ConfigCommand == ConfigCommand.Inherits);
            var visibilityNode = configNodes.LastOrDefault(x => x.ConfigCommand == ConfigCommand.Visibility)
                as VisibilityNode;
            return new Config
                   {
                       Usings = usings.ToList(),
                       InheritsNode = inheritsNode as InheritsNode,
                       Visibility = visibilityNode?.Visibility ?? "internal"
                   };

        }
    }
}
