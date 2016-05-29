namespace RcsGen.SyntaxTree.States.KeywordStates.ForStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.AtStates;
    using RcsGen.SyntaxTree.States.AtStates.ForStates;
    using RcsGen.SyntaxTree.States.BracketStates;

    internal class ForConditionState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly string keyword;
        private readonly List<Node> nodes;
        private readonly BracketStateFactory factory;

        public ForConditionState(StateMachine stateMachine, IState previous, string keyword, List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.keyword = keyword;
            this.nodes = nodes;
            factory = new BracketStateFactory(stateMachine, this, BracketStateFactory.AllBrackets);
        }

        public override void ProcessToken(string token)
        {
            if (token != ")")
            {
                Accumulate(token);
                factory.TryBracket(token);
                return;
            }

            var childNodes = new List<Node>();
            stateMachine
                .Expect("{", previous)
                .SuccessState = new ChildNodesState(stateMachine, childNodes)
                                {
                                    ReturnAction = () =>
                                    {
                                        nodes.Add(new ForNode(keyword, Accumulated, childNodes));
                                        stateMachine.State = previous;
                                    }
                                };
        }
    }
}
