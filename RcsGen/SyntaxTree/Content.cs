namespace RcsGen.SyntaxTree
{
    internal class Content
    {
        public Content(string input, int start, int count)
        {
            Input = input;
            Start = start;
            Count = count;
        }

        public string Input { get; set; }

        public int Start { get; set; }

        public int Count { get; set; }
    }
}
