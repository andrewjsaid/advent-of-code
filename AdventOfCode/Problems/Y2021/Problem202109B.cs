namespace AdventOfCode.Problems.Y2021;

internal class Problem202109B : IProblem
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

        var basinId = new int[hLength, vLength];
        var nextId = 1;
        for (int x = 0; x < hLength; x++)
        {
            for (int y = 0; y < vLength; y++)
            {
                if (isHmin[x, y] && isVmin[x, y])
                {
                    basinId[x, y] = nextId++;
                }
            }
        }

        bool stop;
        do
        {
            stop = true;
            for (int y = 0; y < vLength; y++)
            {
                for (int x = 0; x < hLength; x++)
                {
                    if (data[x, y] == 9)
                        continue;


                    if (x > 0 && data[x - 1, y] < 9)
                    {
                        var prevX = basinId[x - 1, y];
                        var curr = basinId[x, y];

                        if (curr == 0 && prevX > 0)
                        {
                            stop = false;
                            basinId[x, y] = prevX;
                        }
                        else if (curr > 0 && prevX == 0)
                        {
                            stop = false;
                            basinId[x - 1, y] = curr;
                        }
                        else if (curr > 0 && prevX > 0 && curr != prevX)
                        {
                            stop = false;
                            Merge(basinId, curr, prevX);
                        }
                    }

                    if (y > 0 && data[x, y - 1] < 9)
                    {
                        var prevY = basinId[x, y - 1];
                        var curr = basinId[x, y];

                        if (curr == 0 && prevY > 0)
                        {
                            stop = false;
                            basinId[x, y] = prevY;
                        }
                        else if (curr > 0 && prevY == 0)
                        {
                            stop = false;
                            basinId[x, y - 1] = curr;
                        }
                        else if (curr > 0 && prevY > 0 && curr != prevY)
                        {
                            stop = false;
                            Merge(basinId, curr, prevY);
                        }
                    }
                }
            }
        } while (!stop);

        var sizes = new int[nextId - 1];
        for (int x = 0; x < hLength; x++)
        {
            for (int y = 0; y < vLength; y++)
            {
                var id = basinId[x, y];
                if (id > 0)
                {
                    sizes[id - 1]++;
                }
            }
        }

        return sizes.OrderByDescending(x => x).Take(3).Aggregate(1, (a, b) => a * b).ToString();
    }

    private static void Merge(int[,] data, int a, int b)
    {
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                if (data[i, j] == b)
                {
                    data[i, j] = a;
                }
            }
        }
    }
}
