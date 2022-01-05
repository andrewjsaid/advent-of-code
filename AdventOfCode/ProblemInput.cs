using System.Diagnostics;

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

        public int[,] GetIntArr2D()
        {
            var result = new int[_input.Length, _input[0].Length];
            for (var i = 0; i < result.GetLength(0); i++)
            {
                Debug.Assert(_input[i].Length == result.GetLength(1));
                for (var j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = _input[i][j] - '0';
                }
            }
            return result;
        }
    }
}
