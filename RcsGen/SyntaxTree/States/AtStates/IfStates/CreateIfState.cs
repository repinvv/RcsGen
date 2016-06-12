﻿using System.Collections.Generic;
using RcsGen.SyntaxTree.Nodes;

namespace RcsGen.SyntaxTree.States.AtStates.IfStates
{
    using System.Linq;

    internal class CreateIfState : IState
    {
        private readonly StateMachine stateMachine;
        private string condition;
        private NodeStore ifNodes;
        private readonly NodeStore parentNodes;
        private readonly IState previous;

        public CreateIfState(StateMachine stateMachine,
            string condition, NodeStore ifNodes,
            NodeStore parentNodes, 
            IState previous)
        {
            this.stateMachine = stateMachine;
            this.condition = condition;
            this.ifNodes = ifNodes;
            this.parentNodes = parentNodes;
            this.previous = previous;
        }

        public void ProcessToken(string token)
        {
            CreateIf();
            previous.ProcessToken(token);
        }

        private void CreateIf()
        {
            var hasEol = ifNodes.HasEol();
            parentNodes.Add(new IfNode(condition, ifNodes), hasEol);
            stateMachine.State = previous;
        }

        public void Finish()
        {
            CreateIf();
            previous.Finish();
        }
    }
}
