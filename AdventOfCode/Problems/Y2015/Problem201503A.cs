namespace AdventOfCode.Problems.Y2015;

internal class Problem201503A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var directions = input.GetString();
        return new HashSet<(int x, int y)>(Run(directions)).Count.ToString();
    }

    private static IEnumerable<(int x, int y)> Run(IEnumerable<char> directions)
    {
        int x = 0;
        int y = 0;
        yield return (x, y);

        foreach (var direction in directions)
        {
            if (direction == '<')
            {
                x--;
            }
            else if (direction == '>')
            {
                x++;
            }
            else if (direction == '^')
            {
                y++;
            }
            else if (direction == 'v')
            {
                y--;
            }
            yield return (x, y);
        }
    }
}
