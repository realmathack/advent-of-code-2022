namespace AdventOfCode.Y2022.Solvers
{
    public class Day06 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            return GetMarkerPosition(input, 4);
        }

        public override object SolvePart2(string input)
        {
            return GetMarkerPosition(input, 14);
        }

        private static int GetMarkerPosition(string input, int charCount)
        {
            for (int i = 0; i < input.Length - charCount; i++)
            {
                var checkSet = new HashSet<char>(input.Substring(i, charCount));
                if (checkSet.Count == charCount)
                {
                    return i + charCount;
                }
            }
            return 0;
        }
    }
}
