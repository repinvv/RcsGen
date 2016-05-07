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
            if (ch != ' ' || !ProcessConfigKeywords(new string(symbols.ToArray())))
            {
                base.ProcessChar(ch);
            }
        }

        private bool ProcessConfigKeywords(string keyword)
        {
            switch (keyword)
            {
                case KeywordConstants.Config.Inherits:
                    stateMachine.CurrentState = new GotConfigState(ConfigCommand.Inherits, nodes, stateMachine, previous);
                    return true;
                case KeywordConstants.Config.Using:
                    stateMachine.CurrentState = new GotConfigState(ConfigCommand.Using, nodes, stateMachine, previous);
                    return true;
                case KeywordConstants.Config.Visibility:
                    stateMachine.CurrentState = new GotConfigState(ConfigCommand.Visibility, nodes, stateMachine, previous);
                    return true;
                default:
                    return false;
            }
        }
    }
}
