﻿using System.Collections.Generic;
using RcsGen.SyntaxTree.Nodes;

namespace RcsGen.SyntaxTree.States.AtStates.IfStates
{
    using System.Linq;
    using RcsGen.SyntaxTree.States.AtStates.Expect;
    using RcsGen.SyntaxTree.States.NodesStates;

    internal class ElseState : IState
    {
        private readonly string condition;
        private readonly NodeStore ifNodes;
        private readonly NodeStore parentNodes;
        private readonly IState ifState;
        private readonly IState previous;
        private readonly StateMachine stateMachine;
        private readonly NodeStore elseNodes;

        public ElseState(StateMachine stateMachine,
            string condition, NodeStore ifNodes, NodeStore parentNodes, IState ifState, IState previous)
        {
            this.stateMachine = stateMachine;
            this.condition = condition;
            this.ifNodes = ifNodes;
            this.parentNodes = parentNodes;
            this.ifState = ifState;
            this.previous = previous;
            this.elseNodes = new NodeStore();
        }

        public void ProcessToken(string token)
        {
            stateMachine
                .Expect("{", ifState)
                .SuccessState = new SingleLineChildNodesState(stateMachine, elseNodes, previous)
                {
                    ReturnAction = CreateNode
                };
            stateMachine.ProcessToken(token);
        }

        private void CreateNode()
        {
            var hasEol = ifNodes.HasEol() || elseNodes.HasEol();
            parentNodes.Add(new IfNode(condition, ifNodes, elseNodes), hasEol);
            stateMachine.State = previous;
        }

        public void Finish()
        {
            CreateNode();
            previous.Finish();
        }
    }
}
