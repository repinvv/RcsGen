using System.Collections.Generic;

namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;

    internal static class StateExtensions
    {
        public static List<string> SplitBySpace(this string src)
        {
            return src.Trim()
                      .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                      .ToList();
        }

        public static List<Tuple<string, string>> CreateParameters(this string src)
        {
            return src
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .Where(x => x.Length == 2)
                .Select(x => new Tuple<string, string>(x[0], x[1]))
                .ToList();
        }

        public static bool HasEol(this NodeStore nodes) 
            => nodes.Nodes.Any(x => x.NodeType == NodeType.Eol || x.NodeType == NodeType.ForceEol);
    }
}
