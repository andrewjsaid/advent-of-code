namespace AdventOfCode.Problems
{
    internal class Problem202108A : IProblem
    {
        public string Solve(ProblemInput input)
        {
            var entries = Array.ConvertAll(input.GetStrings(), ParsingEntry.Parse);

            return entries.Sum(e => e.Outputs.Count(o => o.Length == 2 || o.Length == 4 || o.Length == 3 || o.Length == 7)).ToString();
        }

        private record ParsingEntry(string[] Possibilities, string[] Outputs)
        { 
            public static ParsingEntry Parse(string line)
            {
                var parts = line.Split(" | ");
                var possibilities = parts[0].Split(' ');
                var outputs = parts[1].Split(' ');
                return new ParsingEntry(possibilities, outputs);
            }
        }
    }
}
