namespace AdventOfCode.Problems.Y2021;

internal class Problem202109A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var lines = input.GetStrings();
        var vLength = lines.Length;
        var hLength = lines[0].Length;

        var data = new int[hLength, vLength];

        for (int x = 0; x < hLength; x++)
        {
            for (int y = 0; y < vLength; y++)
            {
                data[x, y] = lines[y][x] - '0';
            }
        }

        var isHmin = new bool[hLength, vLength];

        for (int y = 0; y < vLength; y++)
        {
            var prev2 = 9;
            var prev1 = data[0, y];
            for (int x = 1; x < hLength; x++)
            {
                var current = data[x, y];
                if (prev1 < prev2 && prev1 < current)
                {
                    // prev1 was a minimum
                    isHmin[x - 1, y] = true;
                }
                prev2 = prev1;
                prev1 = current;
            }

            if (prev1 < prev2)
            {
                // also check last datum
                isHmin[hLength - 1, y] = true;
            }
        }

        var isVmin = new bool[hLength, vLength];

        for (int x = 0; x < hLength; x++)
        {
            var prev2 = 9;
            var prev1 = data[x, 0];
            for (int y = 1; y < vLength; y++)
            {
                var current = data[x, y];
                if (prev1 < prev2 && prev1 < current)
                {
                    // prev1 was a minimum
                    isVmin[x, y - 1] = true;
                }
                prev2 = prev1;
                prev1 = current;

            }

            if (prev1 < prev2)
            {
                // also check last datum
                isVmin[x, vLength - 1] = true;
            }
        }

        var result = 0;
        for (int x = 0; x < hLength; x++)
        {
            for (int y = 0; y < vLength; y++)
            {
                if (isHmin[x, y] && isVmin[x, y])
                {
                    result += data[x, y] + 1;
                }
            }
        }
        return result.ToString();
    }
}
