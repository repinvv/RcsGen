namespace RcsGen.SyntaxTree.States
{
    using System.Collections.Generic;

    internal abstract class AccumulatingState : IAccumulatingState
    {
        private readonly List<char> symbols = new List<char>();

        protected string Accumulated => new string(symbols.ToArray());

        protected void Clear() => symbols.Clear();

        public abstract void ProcessChar(char ch);

        public void Accumulate(char ch) => symbols.Add(ch);
    }
}
