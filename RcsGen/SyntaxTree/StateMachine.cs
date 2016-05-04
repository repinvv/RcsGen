namespace RcsGen.SyntaxTree
{
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States;

    internal class StateMachine
    {
        public StateMachine()
        {
            CurrentState = new DocumentState(this, Document);
        }

        public Document Document { get; } = new Document();

        public IState CurrentState { get; set; }

        public void ProcessChar(char ch)
        {
            CurrentState.ProcessChar(ch);
        }
    }
}
