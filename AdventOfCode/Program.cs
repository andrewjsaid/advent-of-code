using AdventOfCode;
using System.Reflection;

Console.Write("Hello! Please enter a problem number e.g. 202101A: ");
var problemNumber = Console.ReadLine();

var problemType = Type.GetType($"AdventOfCode.Problems.Problem{problemNumber}");
if (problemType is null)
{
    Console.WriteLine("Problem implementation was not found.");
    return;
}

var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream($"AdventOfCode.Data.{problemNumber}.txt");
if (resource is null)
{
    Console.WriteLine("Problem input not found");
    return;
}

ProblemInput problemInput = new(await new StreamReader(resource).ReadToEndAsync());

string result;

var activated = Activator.CreateInstance(problemType);
if (activated is IProblem problem)
{
    result = problem.Solve(problemInput);
}
else if (activated is IAsyncProblem asyncProblem)
{
    result = await asyncProblem.SolveAsync(problemInput);
}
else
{
    Console.WriteLine("Problem does not implement required interface");
    return;
}

Console.WriteLine($"Result: {result}");
