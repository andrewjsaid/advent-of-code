using AdventOfCode;
using System.Reflection;

var defaultProblem = Assembly.GetExecutingAssembly().GetTypes()
    .Where(n => n.Namespace == "AdventOfCode.Problems" && n.Name.StartsWith("Problem"))
    .Select(t => t.Name)
    .OrderByDescending(t => t)
    .First()
    .Substring("Problem".Length);

Console.Write($"Hello! Please enter a problem number. Leave blank for {defaultProblem}: ");
var problemNumber = Console.ReadLine();
if (string.IsNullOrEmpty(problemNumber))
{
    problemNumber = defaultProblem;
}

var problemType = Type.GetType($"AdventOfCode.Problems.Problem{problemNumber}");
if (problemType is null)
{
    Console.WriteLine("Problem implementation was not found.");
    return;
}

var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream($"AdventOfCode.Data.{problemNumber}.txt");
if (resource is null)
{
    resource = Assembly.GetExecutingAssembly().GetManifestResourceStream($"AdventOfCode.Data.{problemNumber[0..^1]}.txt");
    if (resource is null)
    {
        Console.WriteLine("Problem input not found");
        return;
    }
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
