namespace RcsGen.SyntaxTree.States.BracketStates
{
    internal class RoundParenthesisState : BracketState
    {
        public RoundParenthesisState(StateMachine stateMachine, IAccumulatingState previous)
            : base(stateMachine, previous, ')', BracketStateFactory.AllBrackets)
        {
        }
    }
}
