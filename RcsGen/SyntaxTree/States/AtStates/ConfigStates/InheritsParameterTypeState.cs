namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal class InheritsParameterTypeState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly List<Tuple<string, string>> parameters;
        private readonly string baseClass;
        private readonly List<Node> nodes;

        public InheritsParameterTypeState(StateMachine stateMachine, 
            IState previous, 
            List<Tuple<string,string>> parameters,
            string baseClass,
            List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.parameters = parameters;
            this.baseClass = baseClass;
            this.nodes = nodes;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case ")":
                case "\n":
                    nodes.Add(new InheritsNode(baseClass, parameters));
                    stateMachine.State = previous;
                    break;
                case " ":
                    var nameState = new InheritsParameterNameState(stateMachine, previous, parameters, baseClass, Accumulated, nodes);
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
