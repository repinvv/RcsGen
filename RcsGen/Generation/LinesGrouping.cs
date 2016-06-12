namespace RcsGen.Generation
{
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;

    internal static class LinesGrouping
    {
        public static IEnumerable<List<Node>> GroupLines(this IEnumerable<Node> nodes)
        {
            var list = new List<Node>();
            foreach (var node in nodes)
            {
                list.Add(node);
                if (node.NodeType == NodeType.Eol || node.NodeType == NodeType.ForceEol)
                {
                    yield return list;
                    list = new List<Node>();
                }
            }

            if (list.Any())
            {
                yield return list;
            }
        }
    }
}
