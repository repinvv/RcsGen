namespace RcsGen.LowLevelTree
{
    internal class Content
    {
        public Content(string input, int start, int end)
        {
            Input = input;
            Start = start;
            End = end;
        }

        public Content(Content content, int delta)
        {
            Input = content.Input;
            Start = content.Start + delta;
            End = content.End;
        }

        public string Input { get; }

        public int Start { get; }

        public int Count => End - Start;

        public int End { get; }
    }
}
