namespace RcsGen.Generation
{
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;

    internal static class NodesGenerator
    {
        public static void GenerateNode(this StringGenerator sg, Node node, Config config)
        {
            switch (node.NodeType)
            {
                case NodeType.Literal:
                    var content = (ContentNode)node;
                    sg.AppendLine($"WriteLiteral(\"{content.Content}\");");
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
                    sg.AppendLine(((ContentNode)node).Content);
                    break;
                case NodeType.Partial:
                    sg.AppendLine($"Write({string.Format(config.PartialPattern, ((ContentNode)node).Content)});");
                    break;
            }
        }

        private static bool IsSuppressed(this Node node)
        {
            return node.NodeType == NodeType.Eol
                   || (node.NodeType == NodeType.Literal
                       && string.IsNullOrWhiteSpace(((ContentNode)node).Content));
        }

        public static void GenerateNodes(this StringGenerator sg, IEnumerable<Node> nodes, Config config)
        {
            foreach (var line in nodes.GroupLines())
            {
                if (ShouldSuppressEmptyEntries(line))
                {
                    foreach (var node in line.Where(x=>!x.IsSuppressed()))
                    {
                        sg.GenerateNode(node, config);
                    }
                }
                else
                {
                    foreach (var node in line)
                    {
                        sg.GenerateNode(node, config);
                    }
                }
            }
        }

        private static bool ShouldSuppressEmptyEntries(List<Node> line)
        {
            return line.Any(x=>x.IsSuppressionNode());
        }
    }
}
