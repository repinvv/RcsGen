namespace RcsGen.Generation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;

    internal static class NodesGenerator
    {
        public static void GenerateNode(this StringGenerator sg, Node node, Config config)
        {
            switch (node.NodeType)
            {
                case NodeType.Literal:
                    var content = (ContentNode)node;
                    sg.AppendLine($"WriteLiteral(@\"{content.Content.Replace("\"", "\"\"")}\");");
                    break;
                case NodeType.ForceEol:
                    sg.AppendLine("WriteLiteral(Environment.NewLine);");
                    break;
                case NodeType.Eol:
                    sg.AppendLine("WriteLiteral(Environment.NewLine);");
                    break;
                case NodeType.WriteExpression:
                    var expr = (ContentNode)node;
                    sg.AppendLine($"Write({expr.Content});");
                    break;
                case NodeType.For:
                    var forNode = (ForNode)node;
                    sg.AppendLine($"{forNode.Keyword} ({forNode.Condition})");
                    sg.Braces(x => x.GenerateNodes(forNode.ChildNodes.Nodes, config));
                    break;
                case NodeType.If:
                    var ifNode = (IfNode)node;
                    sg.AppendLine($"if ({ifNode.Condition})");
                    sg.Braces(x => x.GenerateNodes(ifNode.IfNodes.Nodes, config));
                    if (!ifNode.ElseNodes.Nodes.Any())
                    {
                        break;
                    }

                    sg.AppendLine("else");
                    sg.Braces(x => x.GenerateNodes(ifNode.ElseNodes.Nodes, config));
                    break;
                case NodeType.CodeExpression:
                    sg.AppendLine(((ContentNode)node).Content.Replace("\n", Environment.NewLine));
                    break;
                case NodeType.Partial:
                    sg.AppendLine($"Write({string.Format(config.PartialPattern, ((ContentNode)node).Content)});");
                    break;
            }
        }

        private static bool IsSuppressable(this Node node)
        {
            return node.NodeType == NodeType.Eol
                   || (node.NodeType == NodeType.Literal
                       && string.IsNullOrWhiteSpace(((ContentNode)node).Content));
        }

        public static void GenerateNodes(this StringGenerator sg, IEnumerable<Node> nodes, Config config)
        {
            bool empty = true;
            foreach (var line in nodes.GroupLines())
            {
                var resultline = ShouldSuppressEmptyEntries(line) 
                    ? line.Where(x => !x.IsSuppressable()) 
                    : line;

                if (empty && line.All(x => x.IsSuppressable()))
                {
                    continue;
                }

                empty = false;

                foreach (var node in resultline)
                {
                    sg.GenerateNode(node, config);
                }
            }
        }

        private static bool IsEmpty(List<Node> line)
        {
            return line.All(x => x.IsSuppressable());
        }

        private static bool ShouldSuppressEmptyEntries(List<Node> line)
        {
            return line.Any(x => x.IsSuppressionNode()) ||
                   (line.Count == 2
                    && line[0].NodeType == NodeType.Partial
                    && line[1].NodeType == NodeType.Eol);
        }
    }
}
