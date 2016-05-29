namespace RcsGen.SyntaxTree.States.AtStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.AtStates.Keywords;
    using RcsGen.SyntaxTree.States.KeywordStates.ForStates;

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
                case "\t":
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
                    var ifExpectState = new ExpectState(stateMachine, previous, "(");
                    return;
                case KeywordConstants.For:
                case KeywordConstants.Foreach:
                    stateMachine.Expect("(", previous)
                        .SuccessState = new ForConditionState(stateMachine, previous, token, nodes);
                    return;
                default:
                    var state = new ImplicitWriteState(nodes, stateMachine, previous);
                    stateMachine.State = state;
                    state.ProcessToken(token);
                    return;
            }
        }
    }
}
