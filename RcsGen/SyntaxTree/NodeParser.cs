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

        public static IEnumerable<INode> ParseNode(Content content)
        {
            if (content.Count < 1 || content.Input[content.Start + 1] == ' ')
            {
                throw new Exception("Syntax error, @ sign, position " + content.Start);
            }

            if (content.Input[content.Start + 1] == '{')
            {
                
            }

            if (content.Input[content.Start + 1] == '*')
            {

            }

            KeywordParser.CheckKeyword(content, IfKw, ProcessIf);
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

        private static IEnumerable<INode> ProcessIf(Content content, string condition)

        private static int KeywordDisposition(string input, int start, int count, string keyword)
        {
            var kwPresent = string.Compare(keyword, 0, input, start, Math.Max(count, keyword.Length));
            //if(kwPresent)
            return 0;
        }
    }
}
