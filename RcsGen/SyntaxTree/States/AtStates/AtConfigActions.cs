namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;
    using RcsGen.SyntaxTree.States.AtStates.ConfigStates;

    internal class AtConfigActions
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly List<Node> nodes;

        public AtConfigActions(StateMachine stateMachine, IState previous, List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.nodes = nodes;
        }

        public void GotoInherits() => stateMachine
            .State = new SkipSpacesState(stateMachine, new InheritsState(stateMachine, previous, nodes));

        public void CreateUsingNode(string content)
        {
            nodes.Add(new UsingNode(content.SplitBySpace()));
            stateMachine.State = previous;
        }

        public void GotoUsing() => stateMachine
            .State = new ContentState(stateMachine, "\n", CreateUsingNode);

        public void GotoVisibility() => stateMachine
            .State = new VisibilityState(nodes, stateMachine, previous);

        public void GotoImplements() => stateMachine
            .State = new ImplementsState(nodes, stateMachine, previous);

        public void GotoConstructor() => stateMachine
            .State = new ConstructorParametersState(stateMachine, previous, nodes);

        public void GotoMember() => stateMachine
            .ExpectAtSameLine("{", previous)
            .SuccessState = new MemberState(stateMachine, previous, nodes);

        public void GotoPartial()
        { }
    }
}
