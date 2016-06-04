namespace RcsGen.SyntaxTree.States.BracketStates
{
    internal class SquareBracketState : BracketState
    {
        public SquareBracketState(StateMachine stateMachine, IAccumulatingState previous) 
            : base(stateMachine, previous, "]", BracketStateFactory.AllBrackets)
        { }
    }
}
