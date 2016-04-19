namespace RcsGen.SyntaxTree
{
    using System.Collections.Generic;
    using Nodes;

    internal static class Parser
    {
        public static IEnumerable<INode> Parse(string input, int start, int count)
        {
            if (string.IsNullOrEmpty(input) || count == 0)
            {
                yield break;
            }

            var at = input.IndexOf('@', start, count);
            if (at == -1)
            {
                yield return new SimpleNode {NodeType = NodeType.Literal, Content = input};
                yield break;
            }

            if (at > start)
            {
                yield return new SimpleNode { NodeType = NodeType.Literal, Content = input.Substring(start, at - start) };
            }

            foreach (var node in NodeParser.ParseNode(input, at, count + start - at))
            {
                yield return node;
            }
        }
    }
}
