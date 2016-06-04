namespace RcsGen.Generation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class ConstructorGenerator
    {
        public static void GenerateConstructor(this StringGenerator sg, Config config, string className)
        {
            var ownParms = config.ConstructorParametersNode?.ConstructorParameters.ToList() 
                ?? new List<Tuple<string, string>>();
            var baseParms = config.InheritsNode?.ConstructorParameters.ToList()
                ?? new List<Tuple<string, string>>();
            var allParams = ownParms.Concat(baseParms).ToList();
            if (!allParams.Any())
            {
                return;
            }

            sg.AppendLine("#region constructor");
            allParams.ForEach(x=> sg.AppendLine(x.Item1 + " " + x.Item2 + ";"));
            sg.AppendLine();
            sg.AppendLine($"public {className}({GetParamsString(allParams)})");
            if (config.InheritsNode?.ConstructorParameters.Any() == true)
            {
                sg.PushIndent();
                sg.AppendLine($": base({GetBaseParams(baseParms)})");
                sg.PopIndent();
            }

            sg.Braces(x => sg.GenerateAssignments(allParams));
            sg.AppendLine("#endregion");
            sg.AppendLine();
        }

        private static void GenerateAssignments(this StringGenerator sg, List<Tuple<string, string>> allParams)
        {
            allParams.ForEach(x => sg.AppendLine($"this.{x.Item2} = {x.Item2};"));
        }

        private static string GetBaseParams(IEnumerable<Tuple<string, string>> parameters) 
            => string.Join(", ", parameters.Select(x => x.Item2));

        private static string GetParamsString(IEnumerable<Tuple<string, string>> parameters) 
            => string.Join(", ", parameters.Select(x => x.Item1 + " " + x.Item2));
    }
}
