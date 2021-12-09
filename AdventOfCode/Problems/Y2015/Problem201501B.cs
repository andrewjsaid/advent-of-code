namespace AdventOfCode.Problems.Y2015;

internal class Problem201501B : IProblem
{
    public string Solve(ProblemInput input)
    {
        var result = 0;
        string str = input.GetString();
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
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

            if (result == -1)
            {
                return (i + 1).ToString();
            }
        }
        throw new Exception("Result not found");
    }
}
