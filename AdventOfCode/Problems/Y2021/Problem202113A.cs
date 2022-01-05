using System.Diagnostics;

namespace AdventOfCode.Problems.Y2021;

internal class Problem202113A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var dots = new HashSet<(int x, int y)>();
        var folds = new List<(bool foldX, int length)>();

        var coordinates = true;
        foreach (var line in input.GetStrings())
        {
            if (line == string.Empty)
            {
                coordinates = false;
            }
            else if (coordinates)
            {
                var indexOf = line.IndexOf(',');
                var left = line[..indexOf];
                var right = line[(indexOf + 1)..];
                dots.Add((int.Parse(left), int.Parse(right)));
            }
            else
            {
                Debug.Assert(line.StartsWith("fold along "));
                var fold = line["fold along ".Length..];
                folds.Add((fold[0] == 'x', int.Parse(fold[2..])));
                break; // first fold only for this part
            }
        }

        foreach (var (foldX, length) in folds)
        {
            var newDots = new HashSet<(int x, int y)>();
            if (foldX)
            {
                foreach (var (x, y) in dots)
                {
                    Debug.Assert(x != length);
                    var newX = x < length ? x : length - (x - length);
                    newDots.Add((newX, y));
                }
            }
            else
            {
                foreach (var (x, y) in dots)
                {
                    Debug.Assert(y != length);
                    var newY = y < length ? y : length - (y - length);
                    newDots.Add((x, newY));
                }
            }
            dots = newDots;
        }

        return dots.Count.ToString();
    }
}