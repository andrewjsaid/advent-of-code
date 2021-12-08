namespace AdventOfCode.Problems
{
    internal class Problem202105A : IProblem
    {
        public string Solve(ProblemInput input)
        {
            var lines = Array.ConvertAll(input.GetStrings(), Line2D.Parse);
            var lenX = lines.Max(l => Math.Max(l.X1, l.X2)) + 1;
            var lenY = lines.Max(l => Math.Max(l.Y1, l.Y2)) + 1;

            var grid = new int[lenX, lenY];
            foreach (var line in lines)
            {
                if (line.IsHorizontal)
                {
                    foreach (var x in line.EnumerateX())
                    {
                        grid[x, line.Y1]++;
                    }
                }
                else if (line.IsVertical)
                {
                    foreach (var y in line.EnumerateY())
                    {
                        grid[line.X1, y]++;
                    }
                }
            }

            var result = 0;

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] > 1)
                    {
                        result++;
                    }
                }
            }

            return result.ToString();
        }

        private record Line2D(int X1, int Y1, int X2, int Y2)
        {

            public bool IsHorizontal => Y1 == Y2;

            public bool IsVertical => X1 == X2;

            public bool IsDiagonal => !IsVertical && !IsHorizontal;

            public (int xLower, int xUpper) GetXs()
            {
                if (X1 <= X2)
                {
                    return (X1, X2);
                }
                else
                {
                    return (X2, X1);
                }
            }

            public (int yLower, int yUpper) GetYs()
            {
                if (Y1 <= Y2)
                {
                    return (Y1, Y2);
                }
                else
                {
                    return (Y2, Y1);
                }
            }

            public IEnumerable<int> EnumerateX()
            {
                var (from, to) = GetXs();
                for (int x = from; x <= to; x++)
                {
                    yield return x;
                }
            }

            public IEnumerable<int> EnumerateY()
            {
                var (from, to) = GetYs();
                for (int y = from; y <= to; y++)
                {
                    yield return y;
                }
            }

            public int LowerX => Math.Min(X1, X2);

            public static Line2D Parse(string formatted)
            {
                var comma1Idx = formatted.IndexOf(',');
                var arrowIdx = formatted.IndexOf(" -> ", comma1Idx);
                var comma2Idx = formatted.IndexOf(',', arrowIdx);
                var x1 = formatted[..comma1Idx];
                var y1 = formatted[(comma1Idx + 1)..arrowIdx];
                var x2 = formatted[(arrowIdx + 4)..comma2Idx];
                var y2 = formatted[(comma2Idx + 1)..];
                return new Line2D(
                    int.Parse(x1),
                    int.Parse(y1),
                    int.Parse(x2),
                    int.Parse(y2));
            }
        }
    }
}
