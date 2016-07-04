namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Collections.Generic;

    internal class AtState : IState
    {
        private readonly IState previous;
        private readonly AtActions actions;
        private readonly Dictionary<string, Action<string>> actionsDict;

        public AtState(StateMachine stateMachine, IState previous, NodeStore nodes)
        {
            this.previous = previous;
            actions = new AtActions(stateMachine, previous, nodes);
            actionsDict = new Dictionary<string, Action<string>>
                          {
                              { "@", actions.CreateLiteral },
                              { "\"", actions.CreateLiteralAndReenterToken },
                              { " ", actions.ReturnAndReenterToken },
                              { "<", actions.ReturnAndReenterToken },
                              { ">", actions.ReturnAndReenterToken },
                              { "]", actions.ReturnAndReenterToken },
                              { ")", actions.ReturnAndReenterToken },
                              { "}", actions.ReturnAndReenterToken },
                              { "'", actions.ReturnAndReenterToken },
                              { "\n", t => actions.CreateAtTokenWithEol() },
                              { "{", t => actions.GotoCodeExpression() },
                              { "*", t => actions.GotoComment() },
                              { "(", t => actions.GotoExplicitWriteExpression() },
                              { "[", t => actions.GotoPartial() },
                              { KeywordConstants.Newline, t => actions.CreateNewLine() },
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

        public void Finish() => previous.Finish();
    }
}
