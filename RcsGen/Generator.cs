namespace RcsGen
{
    using SyntaxTree;

    internal static class Generator
    {
        public static string Generate(string inputFile, string nameSpace)
        {
            var tree = Parser.Parse(new Content(inputFile, 0, inputFile.Length));
            return "//text";
        }
    }
}
