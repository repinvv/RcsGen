﻿namespace RcsGen.Generation
{
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;

    internal static class NodesGenerator
    {
        public static void GenerateNode(this StringGenerator sg, Node node, GenState genState)
        {
            var eol = genState.GeneratedEol;
            genState.GeneratedEol = false;
            switch (node.NodeType)
            {
                case NodeType.Literal:
                    var content = (ContentNode)node;
                    sg.AppendLine($"WriteLiteral(\"{content.Content}\");");
                    break;
                case NodeType.Eol:
                    genState.GeneratedEol = true;
                    if (!eol)
                    {
                        sg.AppendLine("WriteLiteral(Environment.NewLine);");
                    }
                    break;
                case NodeType.WriteExpression:
                    var expr = (ContentNode)node;
                    sg.AppendLine($"Write({expr.Content});");
                    break;
                case NodeType.For:
                    var forNode = (ForNode)node;
                    sg.AppendLine($"{forNode.Keyword} ({forNode.Condition})");
                    sg.Braces(x => x.GenerateChildNodes(forNode.ChildNodes, genState));
                    break;
                case NodeType.If:
                    var ifNode = (IfNode)node;
                    sg.AppendLine($"if ({ifNode.Condition})");
                    sg.Braces(x => x.GenerateChildNodes(ifNode.IfNodes, genState));
                    if (!ifNode.ElseNodes.Any())
                    {
                        break;
                    }

                    sg.AppendLine("else");
                    sg.Braces(x => x.GenerateChildNodes(ifNode.ElseNodes, genState));
                    break;
            }
        }

        private static void GenerateChildNodes(this StringGenerator sg, List<Node> childNodes, GenState genState)
            => childNodes.ForEach(x=> sg.GenerateNode(x, genState));
    }
}