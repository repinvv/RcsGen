﻿namespace RcsGen.Generation
{
    using RcsGen.SyntaxTree.Nodes;

    internal static class FileGenerator
    {
        const string GenerationMark =
@"//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------";

        public static string Generate(Document document, Config config, string nameSpace, string className)
        {
            var sg = new StringGenerator();
            sg.AppendLine(GenerationMark);
            sg.AppendLine("namespace " + nameSpace);
            sg.Braces(x => x.GenerateClass(document, config, className));
            return sg.ToString();
        }
    }
}
