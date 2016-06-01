namespace RcsGen.SyntaxTree.States.AtStates
{
    internal static class ExpectExtension
    {
        public static ExpectState Expect(this StateMachine stateMachine, string expecting, IState rejectState)
        {
            var expectState = new ExpectState(stateMachine, rejectState, expecting);
            stateMachine.State = expectState;
            return expectState;
        }

        public static ExpectAtSameLineState ExpectAtSameLine(this StateMachine stateMachine, string expecting, IState rejectState)
        {
            var expectState = new ExpectAtSameLineState(stateMachine, rejectState, expecting);
            stateMachine.State = expectState;
            return expectState;
        }
    }
}