using System.Collections.Generic;
using RcsGen.SyntaxTree.Nodes;

namespace RcsGen.SyntaxTree.States.AtStates.IfStates
{
    using RcsGen.SyntaxTree.States.NodesStates;

    internal class ElseState : IState
    {
        private readonly string condition;
        private readonly List<Node> ifNodes;
        private readonly List<Node> parentNodes;
        private readonly IState ifState;
        private readonly IState previous;
        private readonly StateMachine stateMachine;

        public ElseState(StateMachine stateMachine,
            string condition, List<Node> ifNodes, List<Node> parentNodes, IState ifState, IState previous)
        {
            this.stateMachine = stateMachine;
            this.condition = condition;
            this.ifNodes = ifNodes;
            this.parentNodes = parentNodes;
            this.ifState = ifState;
            this.previous = previous;
        }

        public void ProcessToken(string token)
        {
            var elseNodes = new List<Node>();
            stateMachine
                .Expect("{", ifState)
                .SuccessState = new SingleLineChildNodesState(stateMachine, elseNodes)
                {
                    ReturnAction = () =>
                    {
                        parentNodes.Add(new IfNode(condition, ifNodes, elseNodes));
                        stateMachine.State = previous;
                    }
                };
            stateMachine.ProcessToken(token);
        }

        public void Finish() { }
    }
}
