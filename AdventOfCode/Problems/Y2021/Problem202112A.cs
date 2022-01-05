namespace AdventOfCode.Problems.Y2021;

internal class Problem202112A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var lines = input.GetStrings();
        
        var edges = new Dictionary<string, List<string>>();

        foreach (var line in lines)
        {
            var indexOf = line.IndexOf('-');
            var left = line[..indexOf];
            var right = line[(indexOf + 1)..];

            if (!edges.TryGetValue(left, out var leftEdges))
            {
                edges.Add(left, leftEdges = new());
            }

            if (!edges.TryGetValue(right, out var rightEdges))
            {
                edges.Add(right, rightEdges = new());
            }

            leftEdges.Add(right);
            rightEdges.Add(left);
        }

        var result = Visit(edges, new(), "start");
        return result.ToString();
    }

    private static int Visit(Dictionary<string, List<string>> edges, Stack<string> path, string current)
    {
        if (current == "end")
            return 1;

        if (char.IsLower(current[0]) && path.Contains(current))
            return 0; // already visited

        path.Push(current);

        var result = 0;

        foreach (var edge in edges[current])
        {
            result += Visit(edges, path, edge);
        }

        path.Pop();

        return result;
    }
}