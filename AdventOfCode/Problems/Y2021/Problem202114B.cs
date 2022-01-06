namespace AdventOfCode.Problems.Y2021;

internal class Problem202114B : IProblem
{
    private const int NumSteps = 40;

    private readonly Dictionary<(char left, char right, int depth), Dictionary<char, long>> _cache = new();
    private readonly Dictionary<(char left, char right), char> _formulae = new();

    public string Solve(ProblemInput input)
    {
        var lines = input.GetStrings();
        var template = lines[0];

        foreach (var line in lines.Skip(2))
        {
            _formulae.Add((line[0], line[1]), line[^1]);
        }

        var grouped = new Dictionary<char, long>();
        for (var i = 0; i < template.Length -1; i++)
        {
            var inner = Run(1, template[i], template[i + 1]);
            Merge(inner, grouped);
        }

        foreach (var c in template)
        {
            grouped.TryGetValue(c, out var value);
            grouped[c] = value + 1;
        }

        return (grouped.Values.Max() - grouped.Values.Min()).ToString();
    }

    private Dictionary<char, long> Run(int step, char left, char right)
    {
        if (step > NumSteps)
        {
            return new();
        }

        var distance = NumSteps - step;
        if (!_cache.TryGetValue((left, right, distance), out var result))
        {
            result = new();

            if (_formulae.TryGetValue((left, right), out var insert))
            {
                var leftPair = Run(step + 1, left, insert);
                var rightPair = Run(step + 1, insert, right);

                result[insert] = 1;

                Merge(leftPair, result);
                Merge(rightPair, result);
            }

            _cache.Add((left, right, distance), result);
        }

        return result;
    }

    private static void Merge(Dictionary<char, long> source, Dictionary<char, long> target)
    {
        foreach (var (c, increment) in source)
        {
            target.TryGetValue(c, out var existing);
            target[c] = existing + increment;
        }
    }


}