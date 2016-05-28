﻿namespace RcsGen.SyntaxTree
{
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;

    internal static class Parser
    {
        public static Document Parse(string source)
        {
            var tokens = Tokenizer.GetTokens(source);
            var stateMachine = new StateMachine();
            foreach (var token in tokens)
            {
                stateMachine.ProcessToken(token);
            }
            if (tokens.Last() != "\n")
            {
                stateMachine.ProcessToken("\n");
            }

            return stateMachine.Document;
        }
    }
}
