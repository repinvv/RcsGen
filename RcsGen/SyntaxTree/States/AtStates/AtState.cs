namespace RcsGen.SyntaxTree.States.AtStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.AtStates.ConfigStates;
    using RcsGen.SyntaxTree.States.AtStates.ForStates;
    using RcsGen.SyntaxTree.States.AtStates.IfStates;

    internal class AtState : IState
    {
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly bool allConfig;

        public AtState(List<Node> nodes, StateMachine stateMachine, IState previous, bool allConfig = false)
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
                    break;
                case " ":
                case "\"":
                case "\t":
                case "<":
                case ">":
                case "[":
                case "]":
                case ")":
                case "}":
                case "'":
                    stateMachine.State = previous;
                    previous.ProcessToken(token);
                    break;
                case "\n":
                    nodes.Add(new ContentNode("@", NodeType.Literal));
                    nodes.Add(new Node(NodeType.Eol));
                    stateMachine.State = previous;
                    break;
                case "{":
                    stateMachine.State = new ExpressionState(stateMachine, previous);
                    break;
                case "*":
                    stateMachine.State = new CommentState(() => stateMachine.State = previous);
                    break;
                case "(":
                    stateMachine.State = new ExplicitWriteState(stateMachine, previous, nodes);
                    break;
                case KeywordConstants.If:
                    stateMachine.Expect("(", previous)
                        .SuccessState = new IfConditionState(stateMachine, previous, nodes);
                    break;
                case KeywordConstants.For:
                case KeywordConstants.Foreach:
                    stateMachine.Expect("(", previous)
                        .SuccessState = new ForConditionState(stateMachine, previous, token, nodes);
                    break;
                default:
                    var state = new ImplicitWriteState(nodes, stateMachine, previous);
                    stateMachine.State = state;
                    state.ProcessToken(token);
                    break;
            }
        }

        public void Finish() { }
    }
}
