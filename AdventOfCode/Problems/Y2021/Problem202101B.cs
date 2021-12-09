namespace AdventOfCode.Problems.Y2021;

internal class Problem202101B : IProblem
{
    public string Solve(ProblemInput input)
    {
        var result = 0;

        var readings = input.GetNumbers();

        for (int i = 3; i < readings.Length; i++)
        {
            var prev = readings[i - 3];
            var current = readings[i];
            if (current > prev)
            {
                result++;
            }
        }

        return result.ToString();
    }
}
