namespace RcsGen.Generation
{
    internal static class BasicMethodsGenerator
    {
        public static void GenerateBasicMembers(this StringGenerator sg)
        {
            sg.AppendLine("private readonly StringBuilder sb = new StringBuilder();");
            sg.AppendLine();
            sg.AppendLine("private void WriteLiteral(string text)");
            sg.Braces(x => x.GenerateWriteLiteralContent());
            sg.AppendLine();
            sg.AppendLine("private void Write(object value)");
            sg.Braces(x=> x.GenerateWriteContent());
            sg.AppendLine();
            sg.AppendLine("public override string ToString()");
            sg.Braces("return sb.ToString();");
        }

        private static void GenerateWriteLiteralContent(this StringGenerator sg)
        {
            sg.AppendLine("if (!string.IsNullOrEmpty(text))");
            sg.Braces("sb.Append(text);");
        }

        private static void GenerateWriteContent(this StringGenerator sg)
        {
            sg.AppendLine("if (value != null)");
            sg.Braces("sb.Append(value);");
        }
    }
}
