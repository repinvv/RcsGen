namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal class InheritsState : AccumulatingState
    {
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private List<Tuple<string, string>> emptyList;

        public InheritsState(List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
            emptyList = new List<Tuple<string, string>>();
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

                    break;
                case "(":
                    stateMachine.State = new InheritsParameterTypeState(stateMachine,
                                                                        previous,
                                                                        emptyList,
                                                                        Accumulated,
                                                                        nodes);
                    break;
                case " ":
                    stateMachine.ExpectAtSameLine("(", previous)
                                .SuccessState = new InheritsParameterTypeState(stateMachine,
                                                                               previous,
                                                                               emptyList,
                                                                               Accumulated,
                                                                               nodes);
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }

        public override void Finish() { }
    }
}
