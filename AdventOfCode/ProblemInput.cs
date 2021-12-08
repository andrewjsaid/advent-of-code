namespace AdventOfCode
{
    internal class ProblemInput
    {
        private readonly string[] _input;

        public ProblemInput(string[] input)
        {
            _input = input;
        }

        public string GetString() => string.Join(Environment.NewLine, _input);

        public string[] GetStrings() => _input;

        public int[] GetNumbers() => Array.ConvertAll(_input, int.Parse);

        public int[] GetNumbersCsv() => Array.ConvertAll(_input[0].Split(','), int.Parse);
    }
}
