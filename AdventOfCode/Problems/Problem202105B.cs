namespace AdventOfCode.Problems
{
    internal class Problem202105B : IProblem
    {
        public string Solve(ProblemInput input)
        {
            var lines = Array.ConvertAll(input.GetStrings(), Line2D.Parse);
            var lenX = lines.Max(l => Math.Max(l.X1, l.X2)) + 1;
            var lenY = lines.Max(l => Math.Max(l.Y1, l.Y2)) + 1;

            var grid = new int[lenX, lenY];
            foreach (var line in lines)
            {
                var iX = line.X1 == line.X2 ? 0 : line.X1 < line.X2 ? 1 : -1;
                var iY = line.Y1 == line.Y2 ? 0 : line.Y1 < line.Y2 ? 1 : -1;

                var length = Math.Max(Math.Abs(line.X1 - line.X2), Math.Abs(line.Y1 - line.Y2)) + 1;

                for (int i = 0; i < length; i++)
                {
                    grid[line.X1 + i * iX, line.Y1 + i * iY]++;
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
