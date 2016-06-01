namespace RcsGen.SyntaxTree.States.AtStates.IfStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.BracketStates;
    using RcsGen.SyntaxTree.States.NodesStates;

    internal class IfConditionState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly List<Node> nodes;
        private readonly BracketStateFactory factory;

        public IfConditionState(StateMachine stateMachine, IState previous, List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
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

            var ifNodes = new List<Node>();
            stateMachine
                .Expect("{", previous)
                .SuccessState = new SingleLineChildNodesState(stateMachine, ifNodes)
                {
                    ReturnAction = () =>
                    {
                        var ifState = new CreateIfState(stateMachine, Accumulated, ifNodes, nodes, previous);
                        var elseState = new ElseState(stateMachine, Accumulated, ifNodes, nodes, ifState, previous);
                        stateMachine.Expect("else", ifState)
                                    .SuccessState = elseState;
                    }
                };
        }

        public override void Finish() { }
    }
}
