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
                    break;
                case "@":
                    if (starred)
                    {
                        back();
                    }

                    break;
                default:
                    starred = false;
                    break;
            }
        }
    }
}
