namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal class InheritsParameterNameState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly List<Tuple<string, string>> parameters;
        private readonly string baseClass;
        private readonly string parameterType;
        private readonly List<Node> nodes;

        public InheritsParameterNameState(StateMachine stateMachine,
            IState previous,
            List<Tuple<string, string>> parameters,
            string baseClass,
            string parameterType,
            List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.parameters = parameters;
            this.baseClass = baseClass;
            this.parameterType = parameterType;
            this.nodes = nodes;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case ")":
                case "\n":
                    AddParameter();
                    nodes.Add(new InheritsNode(baseClass, parameters));
                    stateMachine.State = previous;
                    break;
                case " ":
                case ",":
                    AddParameter();
                    stateMachine.State =
                        new InheritsParameterTypeState(stateMachine, previous, parameters, baseClass, nodes);
                    break;
                default: 
                    Accumulate(token);
                    break;
            }
        }

        private void AddParameter()
        {
            parameters.Add(new Tuple<string, string>(parameterType, Accumulated));
        }

        public override void Finish()
        { }
    }
}
