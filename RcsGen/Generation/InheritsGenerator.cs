namespace RcsGen.Generation
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class InheritsGenerator
    {
        public static string GetInheritLine(this Config config)
        {
            if (config.InheritsNode == null && !config.Interfaces.Any())
            {
                return string.Empty;
            }

            IEnumerable<string> inherits = config.Interfaces;

            if (config.InheritsNode != null)
            {
                inherits = new[] { config.InheritsNode.BaseClass }.Concat(config.Interfaces);
            }

            return " : " + string.Join(", ", inherits);

        }
    }
}
