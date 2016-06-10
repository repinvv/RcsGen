using System.Collections.Generic;

namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Linq;

    internal static class StateExtensions
    {
        public static List<string> SplitBySpace(this string src)
        {
            return src.Trim()
                      .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                      .ToList();
        }
    }
}
