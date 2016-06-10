namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal class InheritsState : AccumulatingState
    {
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly List<Tuple<string, string>> parameters;
        private readonly Action createNode;

        public InheritsState(StateMachine stateMachine, IState previous, List<Node> nodes)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
            parameters = new List<Tuple<string, string>>();
            createNode = () => nodes.Add(new InheritsNode(Accumulated, parameters));
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "\n":
                    var baseClass = Accumulated;
                    if (baseClass != string.Empty)
                    {
                        nodes.Add(new InheritsNode(baseClass));
                    }
                    stateMachine.State = previous;

                    break;
                case "(":
                    stateMachine.State = new ParameterTypeState(stateMachine, parameters, createNode, previous);
                    break;
                case " ":
                    stateMachine
                        .ExpectAtSameLine("(", previous)
                        .SuccessState = new ParameterTypeState(stateMachine, parameters, createNode, previous);
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }

        public override void Finish() { }
    }
}
