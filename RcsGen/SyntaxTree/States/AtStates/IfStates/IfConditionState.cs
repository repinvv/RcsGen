namespace RcsGen.SyntaxTree.States.AtStates.IfStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.AtStates.Expect;
    using RcsGen.SyntaxTree.States.BracketStates;
    using RcsGen.SyntaxTree.States.NodesStates;

    internal class IfConditionState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly NodeStore nodes;
        private readonly BracketStateFactory factory;
        private readonly NodeStore ifNodes;

        public IfConditionState(StateMachine stateMachine, IState previous, NodeStore nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.nodes = nodes;
            this.ifNodes = new NodeStore();
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
            
            stateMachine
                .Expect("{", previous)
                .SuccessState = new SingleLineChildNodesState(stateMachine, ifNodes, previous)
                {
                    ReturnAction = GotChildNodes
                };
        }

        private void GotChildNodes()
        {
            var ifState = new CreateIfState(stateMachine, Accumulated, ifNodes, nodes, previous);
            var elseState = new ElseState(stateMachine, Accumulated, ifNodes, nodes, ifState, previous);
            stateMachine.Expect("else", ifState)
                        .SuccessState = elseState;
        }

        public override void Finish()
        {
            var hasEol = ifNodes.HasEol();
            nodes.Add(new IfNode(Accumulated, ifNodes), hasEol);
            previous.Finish();
        }
    }
}
