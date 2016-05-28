﻿namespace RcsGen.SyntaxTree.States.KeywordStates.ForStates
{

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

        public override void ProcessToken(string token)
        {
            if (token == ")")
            {
                var keyword = Accumulated;
                //var awaitState = new AwaitState(stateMachine, this, previous, "{");
            }
        }
        
    }
}
