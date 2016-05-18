namespace RcsGen.SyntaxTree.States
{
    internal interface IState
    {
        void ProcessToken(string token);
    }
}
