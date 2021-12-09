namespace AdventOfCode.Problems.Y2021;

internal class Problem202108B : IProblem
{
    public string Solve(ProblemInput input)
    {
        var sum = 0;
        foreach (var line in input.GetStrings())
        {
            var options = line.Split(" | ")[0].Split(' ').Select(s => new string(s.ToCharArray().OrderBy(c => c).ToArray())).ToArray();
            var outputs = line.Split(" | ")[1].Split(' ').Select(s => new string(s.ToCharArray().OrderBy(c => c).ToArray())).ToArray();

            // mapping[3] gives the index within options of the symbol for number 3
            var mapping = new int[10];
            mapping[1] = Array.FindIndex(options, s => s.Length == 2);
            mapping[4] = Array.FindIndex(options, s => s.Length == 4);
            mapping[7] = Array.FindIndex(options, s => s.Length == 3);
            mapping[8] = Array.FindIndex(options, s => s.Length == 7);

            // 0,6,9 have 6 signals
            // 2,3,5 have 5 signals

            mapping[6] = Array.FindIndex(options, s => s.Length == 6 && !Contains(s, options[mapping[7]]));
            mapping[0] = Array.FindIndex(options, s => s.Length == 6 && Contains(s, options[mapping[7]]) && Shared(s, options[mapping[4]]) == 3);
            mapping[9] = Array.FindIndex(options, s => s.Length == 6 && Contains(s, options[mapping[7]]) && Shared(s, options[mapping[4]]) == 4);

            mapping[3] = Array.FindIndex(options, s => s.Length == 5 && Contains(s, options[mapping[7]]));
            mapping[2] = Array.FindIndex(options, s => s.Length == 5 && !Contains(s, options[mapping[7]]) && !Contains(options[mapping[6]], s));
            mapping[5] = Array.FindIndex(options, s => s.Length == 5 && !Contains(s, options[mapping[7]]) && Contains(options[mapping[6]], s));

            var d1 = Array.IndexOf(mapping, Array.IndexOf(options, outputs[0]));
            var d2 = Array.IndexOf(mapping, Array.IndexOf(options, outputs[1]));
            var d3 = Array.IndexOf(mapping, Array.IndexOf(options, outputs[2]));
            var d4 = Array.IndexOf(mapping, Array.IndexOf(options, outputs[3]));

            var number = (d1 * 1000) + (d2 * 100) + (d3 * 10) + (d4 * 1);
            sum += number;
        }
        return sum.ToString();
    }

    private static bool Contains(string a, string b) => b.All(a.Contains);

    private static int Shared(string a, string b) => a.Count(b.Contains);

    private static int Pick(string[] options, List<string> set, Func<string, bool> predicate)
    {
        for (int i = 0; i < set.Count; i++)
        {
            var match = predicate(set[i]);
            if (match)
            {
                set.RemoveAt(i);
                return Array.IndexOf(options, set[i]);
            }
        }
        throw new InvalidOperationException();
    }
}
