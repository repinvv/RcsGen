namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal class AtState : IState
    {
        private readonly AtActions actions;
        private readonly Dictionary<string, Action<string>> actionsDict;

        public AtState(StateMachine stateMachine, IState previous, List<Node> nodes)
        {
            actions = new AtActions(stateMachine, previous, nodes);
            actionsDict = new Dictionary<string, Action<string>>
                          {
                              { "@", actions.CreateLiteral },
                              { " ", actions.SkipAtAndReenterToken },
                              { "\"", actions.SkipAtAndReenterToken },
                              { "\t", actions.SkipAtAndReenterToken },
                              { "<", actions.SkipAtAndReenterToken },
                              { ">", actions.SkipAtAndReenterToken },
                              { "]", actions.SkipAtAndReenterToken },
                              { ")", actions.SkipAtAndReenterToken },
                              { "}", actions.SkipAtAndReenterToken },
                              { "'", actions.SkipAtAndReenterToken },
                              { "\n", t => actions.CreateAtTokenWithEol() },
                              { "{", t => actions.GotoCodeExpression() },
                              { "*", t => actions.GotoComment() },
                              { "(", t => actions.GotoExplicitWriteExpression() },
                              { "[", t => actions.GotoPartial() },
                              { KeywordConstants.If, t => actions.GotoIf() },
                              { KeywordConstants.For, actions.GotoFor },
                              { KeywordConstants.Foreach, actions.GotoFor },
                          };
        }

        public virtual void ProcessToken(string token)
        {
            var action = actionsDict.SafeGet(token, actions.GotoImplicitWrite);
            action(token);
        }

        public void Finish() { }
    }
}
