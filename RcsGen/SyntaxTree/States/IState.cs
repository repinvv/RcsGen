namespace RcsGen.SyntaxTree.States
{
    internal interface IState
    {
        void ProcessToken(string token);

        void Finish();
    }
}
