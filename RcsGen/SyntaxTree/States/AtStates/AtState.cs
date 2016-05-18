namespace RcsGen.SyntaxTree.States
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.ExpressionStates;
    using RcsGen.SyntaxTree.States.KeywordStates;

    internal class AtState : IState
    {
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly bool allConfig;

        public AtState(List<Node> nodes, StateMachine stateMachine, IState previous, bool allConfig)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.allConfig = allConfig;
        }

        public void ProcessToken(string token)
        {
            if (allConfig)
            {
                switch (token)
                {
                    case KeywordConstants.Config.Inherits:
                        stateMachine.State =
                            new GotConfigState(ConfigCommand.Inherits, nodes, stateMachine, previous);
                        return;
                    case KeywordConstants.Config.Using:
                        stateMachine.State =
                            new GotConfigState(ConfigCommand.Using, nodes, stateMachine, previous);
                        return;
                    case KeywordConstants.Config.Visibility:
                        stateMachine.State =
                            new GotConfigState(ConfigCommand.Visibility, nodes, stateMachine, previous);
                        return;
                }
            }

            switch (token)
            {
                case "@": 
                    nodes.Add(new ContentNode("@", NodeType.Literal));
                    stateMachine.State = previous;
                    return;
                case " ":
                case "\"":
                case "<":
                case ">":
                case "[":
                case "]":
                case ")":
                case "}":
                case "'":
                    nodes.Add(new ContentNode("@" + token, NodeType.Literal));
                    stateMachine.State = previous;
                    return;
                case "\n":
                    nodes.Add(new ContentNode("@", NodeType.Literal));
                    nodes.Add(new Node(NodeType.Eol));
                    stateMachine.State = previous;
                    return;
                case "{":
                    stateMachine.State = new ExpressionState(stateMachine, previous);
                    return;
                case "*":
                    stateMachine.State = new CommentState(() => stateMachine.State = previous);
                    return;
                case "(":
                    stateMachine.State = new ExplicitWriteState(stateMachine, previous, nodes);
                    return;
                case KeywordConstants.If:
                case KeywordConstants.For:
                case KeywordConstants.Foreach:
                    return;
                default:
                    var state = new KeywordsState(nodes, stateMachine, previous);
                    stateMachine.State = state;
                    state.ProcessToken(token);
                    return;
            }
        }
    }
}
