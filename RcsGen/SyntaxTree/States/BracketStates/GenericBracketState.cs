namespace RcsGen.SyntaxTree.States.BracketStates
{
    internal class GenericBracketState : BracketState
    {
        public GenericBracketState(StateMachine stateMachine, IAccumulatingState previous)
            : base(stateMachine, previous, ">", "<")
        {
        }
    }
}
