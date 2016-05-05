namespace RcsGen.SyntaxTree
{
    using RcsGen.SyntaxTree.Nodes;

    internal static class Parser
    {
        public static Document Parse(string source)
        {
            var stateMachine = new StateMachine();
            foreach (var ch in source)
            {
                stateMachine.ProcessChar(ch);
            }

            stateMachine.ProcessChar('\r');

            return stateMachine.Document;
        }
    }
}
