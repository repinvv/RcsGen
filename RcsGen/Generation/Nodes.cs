namespace RcsGen.Generation
{
    using System.Linq;
    using RcsGen.SyntaxTree;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States;

    internal static class Nodes
    {
        private static bool HaveEol(this NodeStore nodes) 
            => nodes.Nodes.Any(x => x.IsEol() || x.IsMultiline());

        private static bool IsMultiline(this Node node)
        {
            if (node.NodeType == NodeType.For)
            {
                return ((ForNode)node).ChildNodes.HaveEol();
            }

            if (node.NodeType == NodeType.If)
            {
                var ifNode = (IfNode)node;
                return ifNode.IfNodes.HaveEol() || ifNode.ElseNodes.HaveEol();
            }

            return false;
        }

        public static bool IsSuppressionNode(this Node node)
        {
            if (node.NodeType == NodeType.CodeExpression || node.IsMultiline())
            {
                return true;
            }

            return false;
        }
    }
}
