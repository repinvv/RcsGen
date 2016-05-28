namespace RcsGen.SyntaxTree
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Tokenizer
    {
        private static readonly char[] Special =
        {
            '<', '>',
            '(', ')',
            '[', ']',
            '{', '}',
            '"', '\'',
            '@', '\\',
            '\n', ' ',
            '*', ',',
            ';'
        };

        public static List<string> GetTokens(string source)
        {
            source = source.Replace("\r\n", "\n")
                           .Replace("\r", "\n");
            var tokens = new List<string>();
            var chars = new List<char>();
            foreach (var ch in source)
            {
                if (Special.Contains(ch))
                {
                    if (chars.Any())
                    {
                        tokens.Add(new string(chars.ToArray()));
                        chars.Clear();
                    }

                    tokens.Add(new string(new[] { ch }));
                }
                else
                {
                    chars.Add(ch);
                }
            }

            if (chars.Any())
            {
                tokens.Add(new string(chars.ToArray()));
                chars.Clear();
            }

            return tokens;
        }
    }
}
