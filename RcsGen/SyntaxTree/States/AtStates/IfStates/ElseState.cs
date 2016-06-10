using System.Collections.Generic;
using RcsGen.SyntaxTree.Nodes;

namespace RcsGen.SyntaxTree.States.AtStates.IfStates
{
    using System.Linq;
    using RcsGen.SyntaxTree.States.NodesStates;

    internal class ElseState : IState
    {
        private readonly string condition;
        private readonly NodeStore ifNodes;
        private readonly NodeStore parentNodes;
        private readonly IState ifState;
        private readonly IState previous;
        private readonly StateMachine stateMachine;

        public ElseState(StateMachine stateMachine,
            string condition, NodeStore ifNodes, NodeStore parentNodes, IState ifState, IState previous)
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
            var elseNodes = new NodeStore();
            stateMachine
                .Expect("{", ifState)
                .SuccessState = new SingleLineChildNodesState(stateMachine, elseNodes)
                {
                    ReturnAction = () =>
                    {
                        var hasEol = ifNodes.Nodes.Any(x => x.NodeType == NodeType.Eol);
                        hasEol |= elseNodes.Nodes.Any(x => x.NodeType == NodeType.Eol);
                        parentNodes.Add(new IfNode(condition, ifNodes, elseNodes), hasEol);
                        stateMachine.State = previous;
                    }
                };
            stateMachine.ProcessToken(token);
        }

        public void Finish() { }
    }
}
