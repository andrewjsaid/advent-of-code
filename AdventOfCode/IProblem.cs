namespace AdventOfCode;

internal interface IProblem
{
    string Solve(ProblemInput input);
}

internal interface IAsyncProblem
{
    Task<string> SolveAsync(ProblemInput input);
}
