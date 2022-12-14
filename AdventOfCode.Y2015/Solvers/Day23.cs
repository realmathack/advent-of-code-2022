namespace AdventOfCode.Y2015.Solvers
{
    public class Day23 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var registers = new Dictionary<char, int>() { { 'a', 0 }, { 'b', 0 } };
            Execute(input, registers);
            return registers['b'];
        }

        public override object SolvePart2(string[] input)
        {
            var registers = new Dictionary<char, int>() { { 'a', 1 }, { 'b', 0 } };
            Execute(input, registers);
            return registers['b'];
        }

        private static void Execute(string[] input, Dictionary<char, int> registers)
        {
            var pc = 0;
            while (pc >= 0 && pc < input.Length)
            {
                switch (input[pc][..3])
                {
                    case "hlf":
                        registers[input[pc][4]] /= 2;
                        break;
                    case "tpl":
                        registers[input[pc][4]] *= 3;
                        break;
                    case "inc":
                        registers[input[pc][4]]++;
                        break;
                    case "jmp":
                        pc += int.Parse(input[pc][4..]);
                        continue;
                    case "jie":
                        if (registers[input[pc][4]] % 2 == 0)
                        {
                            pc += int.Parse(input[pc][6..]);
                            continue;
                        }
                        break;
                    case "jio":
                        if (registers[input[pc][4]] == 1)
                        {
                            pc += int.Parse(input[pc][6..]);
                            continue;
                        }
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown opcode {input[pc][..2]}!");
                }
                pc++;
            }
        }
    }
}
