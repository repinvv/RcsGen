namespace RcsGen.SyntaxTree
{
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States;

    internal class StateMachine
    {
        public StateMachine()
        {
            State = new DocumentState(this, Document);
        }

        public Document Document { get; } = new Document();

        public IState State { get; set; }

        public void ProcessChar(char ch)
        {
            State.ProcessChar(ch);
        }
    }
}
