namespace RcsGen.SyntaxTree.States
{
    using System.Collections.Generic;

    internal abstract class AccumulatingState : IAccumulatingState
    {
        protected readonly List<string> tokens = new List<string>();

        protected string Accumulated => string.Concat(tokens);

        protected void Clear() => tokens.Clear();

        public abstract void ProcessToken(string token);

        public abstract void Finish();

        public void Accumulate(string token) => tokens.Add(token);
    }
}
