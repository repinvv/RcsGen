namespace RcsGen.LowLevelTree.Nodes
{
    internal class CurlyBraceNode : BracketNode
    {
        public override char Opening => '{';
        public override char Closing => '}';
    }
}
