﻿namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;
    using RcsGen.SyntaxTree.States.AtStates.Expect;
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
             .State = new ContentState(stateMachine, "\n", CreateInheritsNode, previous);

        private void CreateUsingNode(string content) => nodes.Add(new UsingNode(content.SplitBySpace()));

        public void GotoUsing() => stateMachine
            .State = new ContentState(stateMachine, "\n", CreateUsingNode, previous);

        private void CreateInheritsNode(string content)
        {
            var trimmed = content.Trim();
            if(trimmed == string.Empty) return;

            if (!trimmed.Contains("("))
            {
                nodes.Add(new InheritsNode(trimmed.Split(' ').First()));
                return;
            }

            var regex = new Regex(@"([^\)]*)\(([^\)]*)\)");
            var match = regex.Match(trimmed);
            if (!match.Success) return;
            var baseClass = match.Groups[1].Value.Trim();
            var parameters = match.Groups[2].Value.Trim().CreateParameters();
            nodes.Add(new InheritsNode(baseClass, parameters));
        }

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
       {
            var regex = new Regex(@"[^\)]*\(([^\)]*)\)");
            var match = regex.Match(content);
            if (!match.Success) return;
            var parameters = match.Groups[1].Value.Trim().CreateParameters();
            nodes.Add(new ConstructorParametersNode(parameters));
        }

        public void GotoConstructor() => stateMachine
            .State = new ContentState(stateMachine, "\n", CreateConstructorNode, previous);

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
