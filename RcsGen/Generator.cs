namespace RcsGen
{
    using RcsGen.Generation;
    using RcsGen.SyntaxTree;

    public class Generator
    {
        public string Generate(string inputFile, string nameSpace, string fileName)
        {
            var document = Parser.Parse(inputFile);
            var config = GenerationConfig.GetGenerationConfig(document);
            var className = ClassNaming.GetClassName(fileName);
            return FileGenerator.Generate(document, config, nameSpace, className);
        }
    }
}
