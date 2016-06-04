namespace RcsGen.Generation
{
    using System;
    using System.Text;

    internal class StringGenerator 
    {
        public const int IndentSize = 4;

        private StringBuilder builder = new StringBuilder();
        private int indentCount = 0;

        public void PushIndent(int amount = 1)
        {
            indentCount += amount;
        }

        public void PopIndent(int amount = 1)
        {
            indentCount -= amount;
            if (indentCount < 0)
            {
                throw new Exception("Indent error");
            }
        }

        public void Braces(Action<StringGenerator> action, bool semicolon = false)
        {
            AppendLine("{");
            PushIndent();
            action(this);
            PopIndent();
            AppendLine(semicolon ? "};" : "}");
        }

        public void Braces(string line)
        {
            Braces(x => x.AppendLine(line));
        }
        
        public void AppendLine(string line)
        {
            var indent = indentCount * IndentSize;
            if (indent > 0)
            {
                builder.Append(' ', indent);
            }

            builder.AppendLine(line);
        }

        public void AppendLine()
        {
            builder.AppendLine();
        }

        public override string ToString()
        {
            if (indentCount != 0)
            {
                throw new Exception("Indent error");
            }

            var output = builder.ToString();
            builder = new StringBuilder();
            return output;
        }
    }
}
