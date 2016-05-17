namespace RcsGen.SyntaxTree.States.KeywordStates.ForStates
{
    using RcsGen.SyntaxTree.States.ExpectingStates;

    internal class ForConditionState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly string keyword;

        public ForConditionState(StateMachine stateMachine, IState previous, string keyword)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.keyword = keyword;
        }

        public override void ProcessChar(char ch)
        {
            if (ch == ')')
            {
                var keyword = Accumulated;
                //var awaitState = new AwaitState(stateMachine, this, previous, "{");
            }
        }
        
    }
}
