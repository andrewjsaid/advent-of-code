using System.Text;

namespace AdventOfCode.Problems.Y2021;

internal class Problem202103B : IProblem
{
    public string Solve(ProblemInput input)
    {
        var lines = input.GetStrings();

        var oxygenBuilder = new StringBuilder();
        var oxygen = 0;
        while (true)
        {
            if (oxygenBuilder.Length == lines[0].Length)
            {
                oxygen = BinaryStringBuilder.Parse(oxygenBuilder.ToString());
                break;
            }
            var (z, o, f) = Count(lines, oxygenBuilder.ToString());
            if (z + o == 1)
            {
                oxygen = BinaryStringBuilder.Parse(f);
                break;
            }
            if (o >= z)
            {
                oxygenBuilder.Append('1');
            }
            else
            {
                oxygenBuilder.Append('0');
            }
        }

        var co2Builder = new StringBuilder();
        var co2 = 0;
        while (true)
        {
            if (co2Builder.Length == lines[0].Length)
            {
                co2 = BinaryStringBuilder.Parse(co2Builder.ToString());
                break;
            }
            var (z, o, f) = Count(lines, co2Builder.ToString());
            if (z + o == 1)
            {
                co2 = BinaryStringBuilder.Parse(f);
                break;
            }
            if (o < z)
            {
                co2Builder.Append('1');
            }
            else
            {
                co2Builder.Append('0');
            }
        }

        return (oxygen * co2).ToString();
    }

    private static (int zeroes, int ones, string first) Count(string[] lines, string prefix)
    {
        int zeroes = 0;
        int ones = 0;
        string first = null;

        foreach (string line in lines)
        {
            if (line.StartsWith(prefix))
            {
                first ??= line;
                if (line[prefix.Length] == '1')
                {
                    ones++;
                }
                else
                {
                    zeroes++;
                }
            }
        }

        return (zeroes, ones, first ?? string.Empty);
    }
}

