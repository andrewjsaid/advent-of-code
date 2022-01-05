namespace AdventOfCode.Problems.Y2021;

internal class Problem202110A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var lines = input.GetStrings();

        var totalSyntaxScore = 0;

        foreach (var line in lines)
        {
            totalSyntaxScore += ScoreSyntaxError(line);
        }

        return totalSyntaxScore.ToString();
    }

    private static int ScoreSyntaxError(string line)
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
                    return Score(bracket);
                }
            }
        }

        return 0;
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
            Bracket.Rounded => 3,
            Bracket.Square => 57,
            Bracket.Curly => 1197,
            Bracket.Angled => 25137,
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
