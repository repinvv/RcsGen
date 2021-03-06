﻿namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Collections.Generic;

    internal class AtConfigState : IState
    {
        private readonly IState previous;
        private readonly AtState atState;
        private readonly Dictionary<string, Action> actionsDict;

        public AtConfigState(StateMachine stateMachine, IState previous, NodeStore nodes) 
        {
            this.previous = previous;
            var actions = new AtConfigActions(stateMachine, previous, nodes);
            atState = new AtState(stateMachine, previous, nodes);
            actionsDict = new Dictionary<string, Action>
                          {
                              { KeywordConstants.Config.Inherits, actions.GotoInherits },
                              { KeywordConstants.Config.Using, actions.GotoUsing },
                              { KeywordConstants.Config.Visibility, actions.GotoVisibility },
                              { KeywordConstants.Config.Implements, actions.GotoImplements },
                              { KeywordConstants.Config.Constructor, actions.GotoConstructor },
                              { KeywordConstants.Config.Member, actions.GotoMember },
                              { KeywordConstants.Config.PartialPattern, actions.GotoPartial }
                          };
        }

        public void ProcessToken(string token)
        {
            var action = actionsDict.SafeGet(token, () => atState.ProcessToken(token));
            action();
        }

        public void Finish() => previous.Finish();
    }
}
