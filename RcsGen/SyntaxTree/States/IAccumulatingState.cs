namespace RcsGen.SyntaxTree.States
{
    internal interface IAccumulatingState : IState
    {
        void Accumulate(char ch);
    }
}
