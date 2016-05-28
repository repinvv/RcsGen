namespace RcsGen.SyntaxTree.States
{
    using System;

    internal class CommentState : IState
    {
        private readonly Action back;
        bool starred;

        public CommentState(Action back)
        {
            this.back = back;
        }

        public void ProcessToken(string token)
        {
            switch (token)
            {
                case "*":
                    starred = true;
                    return;
                case "@":
                    if (starred)
                    {
                        back();
                    }

                    return;
                default:
                    starred = false;
                    return;
            }
        }
    }
}
