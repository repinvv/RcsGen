namespace RcsGen.LowLevelTree.Extensions
{
    using System.Linq;
    using System.Security.AccessControl;
    using RcsGen.LowLevelTree.Nodes;

    internal static class DocumentParse
    {
        private static readonly char[] ExpectedSymbols = { '@', '{', '(', '"', '\'' };

        public static Document Parse(this Document document, Content content)
        {
            while (true)
            {
                int symbolPosition = GetSymbolPosition(content);
                if (symbolPosition > content.Start)
                {
                    document.Nodes.Add(new LiteralNode(content.Input.Substring(content.Start, symbolPosition - content.Start)));
                }

                if (symbolPosition == content.End)
                {
                    return document;
                }

                switch (content.Input[symbolPosition])
                {
                    case '@':
                        return null;
                    case '{':
                        var node = new CurlyBraceNode();
                        document.Nodes.Add(node);
                        content = node.Parse(content);
                        break;
                    case '"':
                        return null;
                    case '\'':
                        return null;
                }
            }
        }

        private static int GetSymbolPosition(Content content)
        {
            int position = content.Start;
            while (position < content.Count && !ExpectedSymbols.Contains(content.Input[position]))
            {
                position++;
            }

            return position;
        }
    }
}
