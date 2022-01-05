namespace AdventOfCode.Problems.Y2021;

internal class Problem202110B : IProblem
{
    public string Solve(ProblemInput input)
    {
        var lines = input.GetStrings();

        var incompletes = new List<long>();

        foreach (var line in lines)
        {
            var score = IncompleteScore(line);
            if (score > 0)
            {
                incompletes.Add(score);
            }
        }

        return incompletes.OrderBy(x => x).Skip(incompletes.Count / 2).First().ToString();
    }

    private static long IncompleteScore(string line)
    {
        var indents = new Stack<Bracket>();
        foreach (var c in line)
        {
            var (bracket, open) = Parse(c);
            if (open)
            {
                indents.Push(bracket);
            }
            else
            {
                var match = indents.Pop();
                if (match != bracket)
                {
                    return 0;
                }
            }
        }

        long result = 0;
        while (indents.Count > 0)
        {
            var missing = indents.Pop();
            result = (result * 5) + Score(missing);
        }
        return result;
    }

    private static (Bracket bracket, bool open) Parse(char c)
        => c switch
        {
            '(' => (Bracket.Rounded, true),
            ')' => (Bracket.Rounded, false),
            ']' => (Bracket.Square, false),
            '[' => (Bracket.Square, true),
            '{' => (Bracket.Curly, true),
            '}' => (Bracket.Curly, false),
            '<' => (Bracket.Angled, true),
            '>' => (Bracket.Angled, false),
            _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
        };

    private static int Score(Bracket b)
        => b switch
        {
            Bracket.Rounded => 1,
            Bracket.Square => 2,
            Bracket.Curly => 3,
            Bracket.Angled => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(b), b, null)
        };

    enum Bracket
    {
        Rounded,
        Square,
        Curly,
        Angled
    }
}
