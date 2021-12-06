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

            var gamma = new BinaryStringBuilder();
            var epsilon = new BinaryStringBuilder();
            for (int i = 0; i < zeroCount.Length; i++)
            {
                if (oneCount[i] > zeroCount[i])
                {
                    gamma.AddOne();
                    epsilon.AddZero();
                }
                else if (oneCount[i] < zeroCount[i])
                {
                    gamma.AddZero();
                    epsilon.AddOne();
                }
            }

            return (gamma.Build() * epsilon.Build()).ToString();
        }
    }
}
