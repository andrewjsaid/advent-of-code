namespace AdventOfCode.Problems
{
    internal class Problem202106B : IProblem
    {
        public string Solve(ProblemInput input)
        {
            var population = new long[9];

            foreach (var age in input.GetNumbersCsv())
            {
                population[age]++;
            }

            for (int days = 0; days < 256; days++)
            {
                var newPopulation = new long[9];
                newPopulation[0] = population[1];
                newPopulation[1] = population[2];
                newPopulation[2] = population[3];
                newPopulation[3] = population[4];
                newPopulation[4] = population[5];
                newPopulation[5] = population[6];
                newPopulation[6] = population[7] + population[0];
                newPopulation[7] = population[8];
                newPopulation[8] = population[0];
                population = newPopulation;
            }

            return population.Sum().ToString();
        }
    }
}
