namespace AdventOfCode.Problems.Y2015;

internal class Problem201501A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var result = 0;
        foreach (var c in input.GetString())
        {
            if (c == '(')
            {
                result++;
            }
            else if (c == ')')
            {
                result--;
            }
            else
            {
                throw new Exception("Unexpected Input");
            }
        }
        return result.ToString();
    }
}
