using AdventOfCode;
using System.Reflection;

var defaultProblem = Assembly.GetExecutingAssembly().GetTypes()
    .Where(n => n.Namespace?.StartsWith("AdventOfCode.Problems") == true && n.Name.StartsWith("Problem"))
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

var problemType = Type.GetType($"AdventOfCode.Problems.Y{problemNumber[0..4]}.Problem{problemNumber}");
if (problemType is null)
{
    Console.WriteLine("Problem implementation was not found.");
    return;
}

var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream($"AdventOfCode.Data.Y{problemNumber[0..4]}.{problemNumber}.txt");
if (resource is null)
{
    resource = Assembly.GetExecutingAssembly().GetManifestResourceStream($"AdventOfCode.Data.Y{problemNumber[0..4]}.{problemNumber[0..^1]}.txt");
    if (resource is null)
    {
        Console.WriteLine("Problem input not found");
        return;
    }
}

var lines = new List<string>();

using var stream = new StreamReader(resource);
string? line;
while ((line = await stream.ReadLineAsync()) != null)
{
    lines.Add(line);
}

var problemInput = new ProblemInput(lines.ToArray());

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
