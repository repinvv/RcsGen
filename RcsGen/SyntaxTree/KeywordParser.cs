namespace RcsGen.SyntaxTree
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal class KeywordResult
    {
        public bool KeywordPresent { get; set; }

        public IEnumerable<INode> Result { get; set; }
    }

    internal static class KeywordParser
    {
        public static KeywordResult CheckKeyword(Content content, string keyword, Func<Content, string, IEnumerable<INode>> processKeyword)
        {
            if (true)
            {
                return new KeywordResult { Result = processKeyword(content, ""), KeywordPresent = true };
            }
            else
            {
                return new KeywordResult ();
            }
        }
    }
}
