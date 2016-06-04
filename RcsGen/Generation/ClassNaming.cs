namespace RcsGen.Generation
{
    using System.IO;
    using System.Linq;

    internal class ClassNaming
    {
        public static string GetClassName(string filePath)
        {
            var file = Path.GetFileName(filePath);
            return file.Split('.').First();
        }
    }
}
