using System.Text.RegularExpressions;

namespace AdventOfCode.Problems.Y2015;

internal class Problem201502B : IProblem
{
    public string Solve(ProblemInput input)
    {
        var regex = new Regex("^(?<l>\\d+)x(?<w>\\d+)x(?<h>\\d+)$");
        var sides = input.GetStrings().Select(s =>
        {
            var match = regex.Match(s);
            var l = int.Parse(match.Groups["l"].Value);
            var w = int.Parse(match.Groups["w"].Value);
            var h = int.Parse(match.Groups["h"].Value);
            return new { Length = l, Width = w, Height = h };
        });

        return sides.Sum(s =>
        {
            var p1 = 2 * (s.Length + s.Width);
            var p2 = 2 * (s.Length + s.Height);
            var p3 = 2 * (s.Width + s.Height);
            var p = Math.Min(p1, Math.Min(p2, p3));
            var bow = s.Length * s.Width * s.Height;
            return p + bow;
        }).ToString();
    }
}
