namespace AdventOfCode
{
    internal class ProblemInput
    {
        private readonly string _input;

        public ProblemInput(string input)
        {
            _input = input;
        }

        public string GetString() => _input;

        public string[] GetStrings() => _input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        public int[] GetNumbers() => Array.ConvertAll(GetStrings(), int.Parse);

        override public string ToString() => _input;
    }
}
