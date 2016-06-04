namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal class ParameterNameState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly List<Tuple<string, string>> parameters;
        private readonly Action createNode;
        private readonly IState previous;
        private readonly string parameterType;

        public ParameterNameState(StateMachine stateMachine,
            List<Tuple<string, string>> parameters,
            Action createNode,
            IState previous,
            string parameterType)
        {
            this.stateMachine = stateMachine;
            this.parameters = parameters;
            this.createNode = createNode;
            this.previous = previous;
            this.parameterType = parameterType;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "\n":
                    AddParameter();
                    createNode();
                    stateMachine.State = previous;
                    break;
                case ")":
                    AddParameter();
                    createNode();
                    stateMachine.ExpectAtSameLine("\n", previous)
                                .SuccessState = previous;
                    break;
                case " ":
                case ",":
                    AddParameter();
                    var typeState = new ParameterTypeState(stateMachine, parameters, createNode, previous);
                    stateMachine.State = new SkipSpacesState(stateMachine, typeState);
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
