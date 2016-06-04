namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal class ParameterTypeState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly List<Tuple<string, string>> parameters;
        private readonly Action createNode;
        private readonly IState previous;

        public ParameterTypeState(StateMachine stateMachine,
            List<Tuple<string,string>> parameters,
            Action createNode,
            IState previous)
        {
            this.stateMachine = stateMachine;
            this.parameters = parameters;
            this.createNode = createNode;
            this.previous = previous;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case ")":
                case "\n":
                    createNode();
                    stateMachine.State = previous;
                    break;
                case " ":
                    var nameState = new ParameterNameState(stateMachine, parameters, createNode, previous, Accumulated);
                    stateMachine.State = new SkipSpacesState(stateMachine, nameState);
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }

        public override void Finish() { }
    }
}
