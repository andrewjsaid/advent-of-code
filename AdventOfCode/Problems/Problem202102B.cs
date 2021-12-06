namespace AdventOfCode.Problems
{
    internal class Problem202102B : IProblem
    {
        public string Solve(ProblemInput input)
        {
            var depth = 0;
            var horizontal = 0;
            var aim = 0;

            foreach (var instruction in input.GetStrings())
            {
                var (direction, spaces) = ParseInstruction(instruction);
                if (direction == "forward")
                {
                    horizontal += spaces;
                    depth += spaces * aim;
                }
                else if (direction == "up")
                {
                    aim -= spaces;
                }
                else if (direction == "down")
                {
                    aim += spaces;
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
}
