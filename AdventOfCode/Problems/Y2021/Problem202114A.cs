using System.Text;

namespace AdventOfCode.Problems.Y2021;

internal class Problem202114A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var lines = input.GetStrings();
        var template = lines[0];

        KeyValuePair<string, string> ParseLine(string line) => new(line[..2], line[^1..]);
        var formulae = new Dictionary<string, string>(lines.Skip(2).Select(ParseLine));

        var sb = new StringBuilder();

        var step = 1;
        do
        {
            sb.Clear();

            for (var i = 0; i < template.Length - 1; i++)
            {
                sb.Append(template[i]);
                var pair = template[i..(i + 2)];
                if (formulae.TryGetValue(pair, out var insert))
                {
                    sb.Append(insert);
                }
            }
            sb.Append(template[^1]);

            template = sb.ToString();

        } while (++step <= 10);

        var elements = template
            .GroupBy(x => x)
            .Select(g => new { Element = g.Key, Count = g.Count() })
            .ToArray();

        var min = elements.Min(c => c.Count);
        var max = elements.Max(c => c.Count);

        return (max - min).ToString();
    }
}