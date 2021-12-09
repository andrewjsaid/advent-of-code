namespace AdventOfCode.Problems.Y2015;

internal class Problem201503B : IProblem
{
    public string Solve(ProblemInput input)
    {
        var directions = input.GetString();

        var d1 = new List<char>();
        var d2 = new List<char>();

        foreach (var direction in directions)
        {
            d1.Add(direction);
            var d3 = d1;
            d1 = d2;
            d2 = d3;
        }

        var hs = new HashSet<(int x, int y)>();
        hs.UnionWith(Run(d1));
        hs.UnionWith(Run(d2));
        return hs.Count.ToString();
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
