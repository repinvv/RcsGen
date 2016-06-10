namespace RcsGen.Generation
{
    using System;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;

    internal static class ClassGenerator
    {
        public static void GenerateClass(this StringGenerator sg, Document document, Config config, string className)
        {
            sg.GenerateUsings(config);
            sg.AppendLine("[System.CodeDom.Compiler.GeneratedCode(\"SharpRazor\", \"1.0.0.0\")]");
            sg.AppendLine($"{config.Visibility} class {className}{config.GetInheritLine()}");
            sg.Braces(x => x.GenerateClassContents(document, config, className));
        }

        private static void GenerateClassContents(this StringGenerator sg, Document document, Config config, string className)
        {
            sg.GenerateConstructor(config, className);

            if (config.InheritsNode == null)
            {
                sg.GenerateBasicMembers();
            }

            config.Members.ForEach(x => sg.AppendLine(x + Environment.NewLine));
            var overrideString = config.InheritsNode == null ? string.Empty : "override ";
            sg.AppendLine($"public {overrideString}string Execute()");
            sg.Braces(x => sg.GenerateExecute(document, config));
        }

        private static void GenerateExecute(this StringGenerator sg, Document document, Config config)
        {
            var nodes = document.Nodes.Where(x => x.NodeType != NodeType.Config);
            var genState = new GenState();
            foreach (var node in nodes)
            {
                sg.GenerateNode(node, genState, config);
            }

            sg.AppendLine();
            sg.AppendLine(config.InheritsNode == null
                ? "return executed = sb.ToString();"
                : "return ToString();");
        }
    }
}
