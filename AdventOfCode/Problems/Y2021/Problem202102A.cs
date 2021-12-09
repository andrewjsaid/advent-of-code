namespace AdventOfCode.Problems.Y2021;

internal class Problem202102A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var depth = 0;
        var horizontal = 0;

        foreach (var instruction in input.GetStrings())
        {
            var (direction, spaces) = ParseInstruction(instruction);
            if (direction == "forward")
            {
                horizontal += spaces;
            }
            else if (direction == "up")
            {
                depth -= spaces;
            }
            else if (direction == "down")
            {
                depth += spaces;
            }
        }

        return (depth * horizontal).ToString();
    }

    private static (string direction, int spaces) ParseInstruction(string i)
    {
        var split = i.Split(' ');
        return (split[0], int.Parse(split[1]));
    }
}
