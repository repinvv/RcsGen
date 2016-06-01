namespace RcsGen
{
    using RcsGen.SyntaxTree;

    public static class Generator
    {
        public static string Generate(string inputFile, string nameSpace)
        {
            var tree = Parser.Parse(inputFile);
            return "//text";
        }
    }
}
