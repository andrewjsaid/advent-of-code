namespace AdventOfCode.Problems.Y2021;

internal class Problem202112B : IProblem
{
    public string Solve(ProblemInput input)
    {
        var lines = input.GetStrings();
        
        var edges = new Dictionary<string, List<string>>();

        foreach (var line in lines)
        {
            var indexOf = line.IndexOf('-');
            var left = line.Substring(0, indexOf);
            var right = line.Substring(indexOf + 1);

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

        var result = Visit(edges, new(), "start", null);
        foreach (var edge in edges.Keys)
        {
            if (edge != "start" && char.IsLower(edge[0]))
            {
                result += Visit(edges, new(), "start", edge);
            }
        }
        return result.ToString();
    }

    private static int Visit(Dictionary<string, List<string>> edges, Stack<string> path, string current, string? requiredTwice)
    {
        if (current == "end")
        {
            var isValid = requiredTwice == null || path.Count(p => p == requiredTwice) == 2;
            return isValid ? 1 : 0;
        }

        if (char.IsLower(current[0]))
        {
            var numAllowedVisits = current == requiredTwice ? 2 : 1;
            var allowed = path.Count(p => p == current) < numAllowedVisits;
            if (!allowed)
            {
                return 0;
            }
        }

        path.Push(current);

        var result = 0;

        foreach (var edge in edges[current])
        {
            if (edge != "start")
            {
                result += Visit(edges, path, edge, requiredTwice);
            }
        }

        path.Pop();

        return result;
    }
}