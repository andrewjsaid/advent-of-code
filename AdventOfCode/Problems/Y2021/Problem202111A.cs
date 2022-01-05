namespace AdventOfCode.Problems.Y2021;

internal class Problem202111A : IProblem
{
    private const int Flashed = 1000;

    public string Solve(ProblemInput input)
    {
        var levels = input.GetIntArr2D();

        long numFlashes = 0;

        var iteration = 0;
        while (++iteration <= 100)
        {
            for (var i = 0; i < levels.GetLength(0); i++)
            {
                for (var j = 0; j < levels.GetLength(1); j++)
                {
                    Increment(levels, i, j);
                }
            }

            bool newFlashes;
            do
            {
                newFlashes = false;
                for (var i = 0; i < levels.GetLength(0); i++)
                {
                    for (var j = 0; j < levels.GetLength(1); j++)
                    {
                        if (levels[i, j] > 9 && levels[i, j] < Flashed)
                        {
                            Flash(levels, i, j);
                            newFlashes = true;
                        }
                    }
                }
            } while (newFlashes);

            for (var i = 0; i < levels.GetLength(0); i++)
            {
                for (var j = 0; j < levels.GetLength(1); j++)
                {
                    if (levels[i, j] >= Flashed)
                    {
                        levels[i, j] = 0;
                        numFlashes++;
                    }
                }
            }

        }

        return numFlashes.ToString();
    }

    private static void Flash(int[,] levels, int i, int j)
    {
        Increment(levels, i - 1, j - 1);
        Increment(levels, i, j - 1);
        Increment(levels, i + 1, j - 1);
        Increment(levels, i - 1, j);
        Increment(levels, i + 1, j);
        Increment(levels, i - 1, j + 1);
        Increment(levels, i, j + 1);
        Increment(levels, i + 1, j + 1);
        levels[i, j] = Flashed;
    }

    private static void Increment(int[,] levels, int i, int j)
    {
        if (i >= 0 && j >= 0 && i < levels.GetLength(0) && j < levels.GetLength(1))
        {
            levels[i, j]++;
        }
    }
}
