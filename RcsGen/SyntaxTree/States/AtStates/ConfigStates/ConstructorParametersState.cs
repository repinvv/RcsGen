namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal class ConstructorParametersState : IState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly List<Tuple<string, string>> parameters;
        private readonly Action createNode;
        public ConstructorParametersState(StateMachine stateMachine, IState previous, List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            parameters = new List<Tuple<string, string>>();
            createNode = () => nodes.Add(new ConstructorParametersNode(parameters));
        }

        public void ProcessToken(string token)
        {
            switch (token)
            {
                case " ":
                    break;
                case "(":
                    stateMachine.State = new ParameterTypeState(stateMachine, parameters, createNode, previous);
                    break;
                default:
                    stateMachine.State = previous;
                    break;
            }
        }

        public void Finish() { }
    }
}
