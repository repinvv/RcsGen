namespace RcsGen.SyntaxTree.States.KeywordStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal class AllKeywordsState : KeywordsState
    {
        public AllKeywordsState(List<Node> nodes, StateMachine stateMachine, IState previous) 
            : base(nodes, stateMachine, previous)
        { }

        public override void ProcessChar(char ch)
        {
            switch (ch)
            {
                case ' ':
                    ProcessAllKeywords(new string(symbols.ToArray()));
                    return;
                case '(':
                    ProcessCommandKeywords(new string(symbols.ToArray()));
                    return;
                case '<':
                case '"':
                case '\'':
                case '{':
                    return; // todo
                default:
                    symbols.Add(ch);
                    return;
            }
        }

        private void ProcessAllKeywords(string keyword)
        {
            switch (keyword)
            {
                case KeywordConstants.If:
                    var ifNode = new IfNode();
                    return;
                case KeywordConstants.For:
                case KeywordConstants.Foreach:
                    var forNode = new ForNode(keyword);
                    return;
                case KeywordConstants.Config.Inherits:
                    stateMachine.CurrentState = new GotConfigState(ConfigCommand.Inherits, nodes, stateMachine, previous);
                    return;
                case KeywordConstants.Config.Using:
                    stateMachine.CurrentState = new GotConfigState(ConfigCommand.Using, nodes, stateMachine, previous);
                    return;
                case KeywordConstants.Config.Visibility:
                    stateMachine.CurrentState = new GotConfigState(ConfigCommand.Visibility, nodes, stateMachine, previous);
                    return;
                default:
                    return;
            }
        }
    }
}
