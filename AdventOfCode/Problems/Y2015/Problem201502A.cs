using System.Text.RegularExpressions;

namespace AdventOfCode.Problems.Y2015;

internal class Problem201502A : IProblem
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
            var lw = s.Length * s.Width;
            var wh = s.Width * s.Height;
            var lh = s.Length * s.Height;
            return 2 * (lw + wh + lh) + Math.Min(lh, Math.Min(wh, lw));
        }).ToString();
    }
}
