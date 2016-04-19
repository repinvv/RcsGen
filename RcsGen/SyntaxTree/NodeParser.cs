namespace RcsGen.SyntaxTree
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal static class NodeParser
    {
        const string IfKw = "if";
        const string ForKw = "for";
        const string ForEachKw = "foreach";

        public static IEnumerable<INode> ParseNode(string input, int start, int count)
        {
            if (count < 1 || input[start + 1] == ' ')
            {
                throw new Exception("Syntax error, @ sign, position " + start);
            }

            if (input[start + 1] == '{')
            {
                
            }

            if (input[start + 1] == '*')
            {

            }

            var disposition
            if (IsKeyword(input, start + 1, count - 1, IfKw))
            {
                
            }

            if (IsKeyword(input, start + 1, count - 1, ForKw))
            {
                
            }

            if (IsKeyword(input, start + 1, count - 1, ForEachKw))
            {
                
            }

            yield break;
        }

        private static bool KeywordDisposition(string input, int start, int count, string keyword)
        {
            var kwPresent = string.Compare(keyword, 0, input, start, Math.Max(count, keyword.Length));
            if(kwPresent)
            return false;
        }
    }
}
