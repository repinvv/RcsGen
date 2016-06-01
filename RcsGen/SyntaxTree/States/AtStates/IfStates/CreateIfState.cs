using System.Collections.Generic;
using RcsGen.SyntaxTree.Nodes;

namespace RcsGen.SyntaxTree.States.AtStates.IfStates
{
    internal class CreateIfState : IState
    {
        private readonly StateMachine stateMachine;
        private string condition;
        private List<Node> ifNodes;
        private readonly List<Node> parentNodes;
        private readonly IState previous;

        public CreateIfState(StateMachine stateMachine,
            string condition, List<Node> ifNodes,
            List<Node> parentNodes, 
            IState previous)
        {
            this.stateMachine = stateMachine;
            this.condition = condition;
            this.ifNodes = ifNodes;
            this.parentNodes = parentNodes;
            this.previous = previous;
        }

        public void ProcessToken(string token)
        {
            Finish();
            previous.ProcessToken(token);
        }

        public void Finish()
        {
            parentNodes.Add(new IfNode(condition, ifNodes));
            stateMachine.State = previous;
        }
    }
}
