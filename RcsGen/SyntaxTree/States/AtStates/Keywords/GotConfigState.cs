﻿namespace RcsGen.SyntaxTree.States.AtStates.Keywords
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal class GotConfigState : AccumulatingState
    {
        private readonly ConfigCommand command;
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;

        public GotConfigState(ConfigCommand command, List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            this.command = command;
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "\n":
                    var parameters = Accumulated.Trim();
                    if (!string.IsNullOrEmpty(parameters))
                    {
                        nodes.Add(new ConfigNode(command, parameters));
                    }

                    stateMachine.State = previous;
                    return;
                default: 
                    Accumulate(ch);
                    return;
            }
        }
    }
}
