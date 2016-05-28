namespace RcsGen.SyntaxTree.States.BracketStates
{
    internal class CurvedBracketState : BracketState
    {
        public CurvedBracketState(StateMachine stateMachine, IAccumulatingState previous) 
            : base(stateMachine, previous, "}", BracketStateFactory.AllBrackets)
        { }
    }
}
