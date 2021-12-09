namespace AdventOfCode.Problems.Y2021;

internal class Problem202101A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var result = 0;

        var readings = input.GetNumbers();

        var last = int.MaxValue;
        foreach (var reading in readings)
        {
            if (reading > last)
            {
                result++;
            }
            last = reading;
        }

        return result.ToString();
    }
}
