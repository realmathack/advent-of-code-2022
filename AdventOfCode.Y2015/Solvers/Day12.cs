using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Solvers
{
    public class Day12 : SolverWithText
    {
        private readonly Regex _numberRegex = new(@"-?\d+");

        public override object SolvePart1(string input)
        {
            return SumNumbers(input);
        }

        public override object SolvePart2(string input)
        {
            int pos;
            while ((pos = input.IndexOf(":\"red\"")) > -1)
            {
                var depth = -1;
                var start = pos;
                while (depth < 0)
                {
                    var token = input[--start];
                    if (token == '{') { depth++; continue; }
                    if (token == '}') { depth--; continue; }
                }
                depth = 1;
                while (depth > 0)
                {
                    var token = input[pos++];
                    if (token == '{') { depth++; continue; }
                    if (token == '}') { depth--; continue; }
                }
                input = input[..start] + input[pos..];
            }
            return SumNumbers(input);
        }

        private int SumNumbers(string input)
        {
            var sum = 0;
            foreach (Match match in _numberRegex.Matches(input))
            {
                sum += int.Parse(match.Groups[0].Value);
            }
            return sum;
        }
    }
}
