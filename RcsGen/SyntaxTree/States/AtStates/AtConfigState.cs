namespace RcsGen.SyntaxTree.States.AtStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.AtStates.ConfigStates;

    internal class AtConfigState : AtState
    {
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;

        public AtConfigState(List<Node> nodes, StateMachine stateMachine, IState previous) 
            : base(nodes, stateMachine, previous)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case KeywordConstants.Config.Inherits:
                    var inheritState = new InheritsState(nodes, stateMachine, previous);
                    stateMachine.State = new SkipSpacesState(stateMachine, inheritState);
                    break;
                case KeywordConstants.Config.Using:
                    stateMachine.State = new UsingState(nodes, stateMachine, previous);
                    break;
                case KeywordConstants.Config.Visibility:
                    stateMachine.State = new VisibilityState(nodes, stateMachine, previous);
                    break;
                case KeywordConstants.Config.Implements:
                    stateMachine.State = new ImplementsState(nodes, stateMachine, previous);
                    break;
                case KeywordConstants.Config.Constructor:
                    stateMachine.State = new ConstructorParametersState(stateMachine, previous, nodes);
                    break;
                case KeywordConstants.Config.Member:
                    stateMachine.State = new MemberState(stateMachine, previous, nodes);
                    break;
                default:
                    base.ProcessToken(token);
                    break;
            }
        }
    }
}
