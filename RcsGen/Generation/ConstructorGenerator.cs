namespace RcsGen.Generation
{
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal static class ConstructorGenerator
    {
        public static void GenerateConstructor(this StringGenerator sg, InheritsNode node, string className)
        {
            sg.AppendLine($"public {className}({GetConstructorParams(node)})");
            if (node.ConstructorParameters.Any())
            {
                sg.PushIndent();
                sg.AppendLine($": base({GetBaseParams(node)})");
                sg.PopIndent();
            }

            sg.Braces(x => { });
        }

        private static string GetBaseParams(InheritsNode node)
        {
            var parms = node.ConstructorParameters.Select(x => x.Item2);
            return string.Join(", ", parms);
        }

        private static string GetConstructorParams(InheritsNode node)
        {
            var parms = node.ConstructorParameters.Select(x => x.Item1 + " " + x.Item2);
            return string.Join(", ", parms);
        }
    }
}
