namespace AdventOfCode.Problems.Y2021;

internal class Problem202115A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var risk = input.GetIntArr2D();

        var accumulator = new int[risk.GetLength(0), risk.GetLength(1)];

        for (var i = 0; i < risk.GetLength(0); i++)
        {
            for (var j = 0; j < risk.GetLength(1); j++)
            {
                accumulator[i, j] = int.MaxValue / 2;
            }
        }

        accumulator[0, 0] = 0;

        bool stop;
        do
        {
            for (var i = 0; i < risk.GetLength(0); i++)
            {
                for (var j = 0; j < risk.GetLength(1); j++)
                {
                    var value = accumulator[i, j];

                    var riskHere = risk[i, j];

                    if (i > 0)
                    {
                        value = Math.Min(value, accumulator[i - 1, j] + riskHere);
                    }
                    if (i < risk.GetLength(0) - 1)
                    {
                        value = Math.Min(value, accumulator[i + 1, j] + riskHere);
                    }
                    if (j > 0)
                    {
                        value = Math.Min(value, accumulator[i, j - 1] + riskHere);
                    }
                    if (j < risk.GetLength(1) - 1)
                    {
                        value = Math.Min(value, accumulator[i, j + 1] + riskHere);
                    }

                    if (value != accumulator[i, j])
                    {
                        stop = false;
                        accumulator[i, j] = value;
                    }
                }
            }

            stop = true;
        } while (!stop);

        return accumulator[accumulator.GetLength(0) - 1, accumulator.GetLength(1) - 1].ToString();
    }
}