﻿namespace RcsGen.Generation
{
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal static class ClassGenerator
    {
        private static string GetInheritLine(this InheritsNode inheritsNode)
        {
            return " : " + inheritsNode.BaseClass;
        }

        public static void GenerateClass(this StringGenerator sg, Document document, Config config, string className)
        {
            sg.GenerateUsings(config);
            var inherits = config.InheritsNode?.GetInheritLine();
            sg.AppendLine($"{config.Visibility} class {className}{inherits}");
            sg.Braces(x => x.GenerateClassContents(document, config, className));
        }

        private static void GenerateClassContents(this StringGenerator sg, Document document, Config config, string className)
        {
            if (config.InheritsNode == null)
            {
                sg.GenerateBasicMembers();
            }
            else
            {
                sg.GenerateConstructor(config.InheritsNode, className);
            }

            sg.AppendLine();
            sg.AppendLine("public override void Execute()");
            sg.Braces(x => sg.GenerateExecute(document));
        }

        private static void GenerateExecute(this StringGenerator sg, Document document)
        {
            var nodes = document.Nodes.Where(x => x.NodeType != NodeType.Config);
            var genState = new GenState();
            foreach (var node in nodes)
            {
                sg.GenerateNode(node, genState);
            }
        }
    }
}
