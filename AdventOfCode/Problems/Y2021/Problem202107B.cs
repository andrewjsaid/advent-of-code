namespace AdventOfCode.Problems.Y2021;

internal class Problem202107B : IProblem
{
    public string Solve(ProblemInput input)
    {
        var hPositions = input.GetNumbersCsv();

        var length = hPositions.Max() - hPositions.Min() + 1;
        var cumulatives = LoadCumulativeNumbers(length);

        var mean = (int)hPositions.Average();
        var meanCost = CalculateFuelCost(hPositions, mean, cumulatives);
        var meanUpperCost = CalculateFuelCost(hPositions, mean + 1, cumulatives);

        var direction = meanUpperCost < meanCost ? 1 : -1;

        var current = mean;
        var currentCost = meanCost;
        while (true)
        {
            var next = current + direction;
            var nextCost = CalculateFuelCost(hPositions, next, cumulatives);
            if (nextCost > currentCost)
            {
                return currentCost.ToString();
            }
            current = next;
            currentCost = nextCost;
        }
    }

    private static int CalculateFuelCost(int[] hPositions, int target, int[] cumulatives)
    {
        var result = 0;
        foreach (var h in hPositions)
        {
            result += cumulatives[Math.Abs(target - h)];
        }
        return result;
    }

    private static int[] LoadCumulativeNumbers(int count)
    {
        var accumulator = 0;
        var results = new int[count];
        for (int i = 0; i < count; i++)
        {
            accumulator += i;
            results[i] = accumulator;
        }
        return results;
    }
}
