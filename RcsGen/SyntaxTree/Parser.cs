namespace RcsGen.SyntaxTree
{
    using System.Collections.Generic;
    using Nodes;

    internal static class Parser
    {
        public static IEnumerable<INode> Parse(Content content)
        {
            if (string.IsNullOrEmpty(content.Input) || content.Count == 0)
            {
                yield break;
            }

            var at = content.Input.IndexOf('@', content.Start, content.Count);
            if (at == -1)
            {
                yield return new SimpleNode {NodeType = NodeType.Literal, Content = content.Input };
                yield break;
            }

            if (at > content.Start)
            {
                yield return new SimpleNode { NodeType = NodeType.Literal, Content = content.Input.Substring(content.Start, at - content.Start) };
            }

            foreach (var node in NodeParser.ParseNode(new Content(content.Input, at, content.Count + content.Start - at)))
            {
                yield return node;
            }
        }
    }
}
