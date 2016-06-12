namespace RcsGen.SyntaxTree
{
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States;
    using RcsGen.SyntaxTree.States.AtStates;

    internal class NodeStore
    {
        private readonly List<Node> nodes = new List<Node>();

        public IReadOnlyList<Node> Nodes => nodes;

        public bool LineStart => !nodes.Any() || nodes.Last().IsEol();

        public void Add(Node node)
        {
            nodes.Add(node);
        }
    }
}
