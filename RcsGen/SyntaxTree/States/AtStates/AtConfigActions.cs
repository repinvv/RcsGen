namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;
    using RcsGen.SyntaxTree.States.AtStates.ConfigStates;
    using static RcsGen.SyntaxTree.States.BracketStates.BracketStateFactory;

    internal class AtConfigActions
    {
        private static readonly string[] VisibilityModifiers = { "public", "internal" };
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly NodeStore nodes;

        public AtConfigActions(StateMachine stateMachine, IState previous, NodeStore nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.nodes = nodes;
        }

        public void GotoInherits() => stateMachine
            .State = new SkipSpacesState(stateMachine, new InheritsState(stateMachine, previous, nodes));

        private void CreateUsingNode(string content) => nodes.Add(new UsingNode(content.SplitBySpace()));

        public void GotoUsing() => stateMachine
            .State = new ContentState(stateMachine, "\n", CreateUsingNode, previous);

        private void CreateVisibilityNode(string content)
        {
            var visibility = content.Trim().ToLower();
            if (!VisibilityModifiers.Contains(visibility)) return;
            nodes.Add(new VisibilityNode(visibility));
        }

        public void GotoVisibility() => stateMachine
            .State = new ContentState(stateMachine, "\n", CreateVisibilityNode, previous);

        private void CreateImplementsNode(string content)
            => nodes.Add(new ImplementsNode(content.SplitBySpace()));

        public void GotoImplements() => stateMachine
            .State = new ContentState(stateMachine, "\n", CreateImplementsNode, previous);

        private void CreateConstructorNode(string content) 
            => nodes.Add(new ConstructorParametersNode(content.CreateParameters()));

        public void Previous() => stateMachine.State = previous;

        public void GotoConstructor() => stateMachine
            .ExpectAtSameLine("(", previous)
            .SuccessState = new Unexpected(Previous, "\n")
                .State = new ContentState(stateMachine, ")", CreateConstructorNode, previous);

        private void CreateMemberNode(string content) => nodes.Add(new MemberNode(content));

        public void GotoMember() => CurvedContent(CreateMemberNode);

        private void CreatePartialPatternNode(string content)
            => nodes.Add(new PartialPatternNode(content));

        public void GotoPartial() => CurvedContent(CreatePartialPatternNode);

        private void CurvedContent(Action<string> createNode) => stateMachine
            .ExpectAtSameLine("{", previous)
            .SuccessState = new ContentState(stateMachine, "}", createNode, previous, AllBrackets);
    }
}
