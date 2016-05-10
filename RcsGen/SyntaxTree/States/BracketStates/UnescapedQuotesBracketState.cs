namespace RcsGen.SyntaxTree.States.BracketStates
{
    internal class UnescapedQuotesBracketState : BracketState
    {
        public UnescapedQuotesBracketState(StateMachine stateMachine, IAccumulatingState previous) 
            : base(stateMachine, previous, '"')
        { }
    }
}
