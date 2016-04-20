namespace RcsGen.LowLevelTree
{
    using Extensions;
    using Nodes;

    internal static class Parser
    {
        public static Document Parse(Content content)
        {
            return new Document().Parse(content);
        }
    }
}
