namespace RcsGen.Generation
{
    using System.Linq;

    internal static class UsingsGenerator
    {
        private static readonly string[] usings =
        {
            "System",
            "System.Text",
            "System.Linq"
        };

        public static void GenerateUsings(this StringGenerator sg, Config config)
        {
            config.Usings.Concat(usings).ToList().ForEach(sg.AppendUsing);
            sg.AppendLine();
        }

        private static void AppendUsing(this StringGenerator sg, string x)
        {
            sg.AppendLine("using " + x + ";");
        }
    }
}
