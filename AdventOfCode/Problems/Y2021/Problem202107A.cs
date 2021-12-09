namespace AdventOfCode.Problems.Y2021;

internal class Problem202107A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var hPositions = input.GetNumbersCsv();

        var mean = (int)hPositions.Average();
        var meanCost = CalculateFuelCost(hPositions, mean);
        var meanUpperCost = CalculateFuelCost(hPositions, mean + 1);

        var direction = meanUpperCost < meanCost ? 1 : -1;

        var current = mean;
        var currentCost = meanCost;
        while (true)
        {
            var next = current + direction;
            var nextCost = CalculateFuelCost(hPositions, next);
            if (nextCost > currentCost)
            {
                return currentCost.ToString();
            }
            current = next;
            currentCost = nextCost;
        }
    }

    private static int CalculateFuelCost(int[] hPositions, int target)
    {
        var result = 0;
        foreach (var h in hPositions)
        {
            result += Math.Abs(target - h);
        }
        return result;
    }
}
