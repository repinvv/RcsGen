namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal class AtState : IState
    {
        private readonly AtActions actions;
        private readonly Dictionary<string, Action<string>> dict;

        public AtState(List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            actions = new AtActions(stateMachine, previous, nodes);
            dict = new Dictionary<string, Action<string>>
                   {
                       { "@", actions.CreateLiteral },
                       { " ", actions.SkipAtAndReenterToken },
                       { "\"", actions.SkipAtAndReenterToken },
                       { "\t", actions.SkipAtAndReenterToken },
                       { "<", actions.SkipAtAndReenterToken },
                       { ">", actions.SkipAtAndReenterToken },
                       { "[", actions.SkipAtAndReenterToken },
                       { "]", actions.SkipAtAndReenterToken },
                       { ")", actions.SkipAtAndReenterToken },
                       { "}", actions.SkipAtAndReenterToken },
                       { "'", actions.SkipAtAndReenterToken },
                       { "\n", t => actions.CreateAtTokenWithEol() },
                       { "{", t => actions.GotoCodeExpression() },
                       { "*", t => actions.GotoComment() },
                       { "(", t => actions.GotoExplicitWriteExpression() },
                       { KeywordConstants.If, t => actions.GotoIf() },
                       { KeywordConstants.For, actions.GotoFor },
                       { KeywordConstants.Foreach, actions.GotoFor },
                   };
        }

        public virtual void ProcessToken(string token)
        {
            var action = dict.SafeGet(token, actions.GotoImplicitWrite);
            action(token);
        }

        public void Finish() { }
    }
}
