namespace AdventOfCode.Problems
{
    internal class Problem202103A : IProblem
    {
        public string Solve(ProblemInput input)
        {
            var zeroCount = new int[12];
            var oneCount = new int[12];

            foreach (var line in input.GetStrings())
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '0')
                    {
                        zeroCount[i]++;
                    }
                    else if (line[i] == '1')
                    {
                        oneCount[i]++;
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            var gamma = 0;
            var epsilon = 0;
            for (int i = 0; i < zeroCount.Length; i++)
            {
                gamma *= 2;
                epsilon *= 2;
                if (oneCount[i] > zeroCount[i])
                {
                    gamma += 1;
                }
                else if (oneCount[i] < zeroCount[i])
                {
                    epsilon += 1;
                }
            }

            return (gamma * epsilon).ToString();
        }
    }
}
