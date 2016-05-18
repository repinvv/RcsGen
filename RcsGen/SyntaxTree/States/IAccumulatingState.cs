namespace RcsGen.SyntaxTree.States
{
    internal interface IAccumulatingState : IState
    {
        void Accumulate(string token);
    }
}
