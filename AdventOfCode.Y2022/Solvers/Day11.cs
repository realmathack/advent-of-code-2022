using System.Numerics;

namespace AdventOfCode.Y2022.Solvers
{
    public class Day11 : SolverWithSections
    {
        public override object SolvePart1(string[] input)
        {
            var monkeys = ToMonkeys(input);
            var inspections = new long[monkeys.Count];
            Array.Fill(inspections, 0);
            for (int round = 0; round < 20; round++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.Items.TryDequeue(out var item))
                    {
                        inspections[monkey.Number]++;
                        item = monkey.Operation(item) / 3;
                        var target = (item % monkey.TestDivision == 0) ? monkey.TestTrueMonkey : monkey.TestFalseMonkey;
                        monkeys[target].Items.Enqueue(item);
                    }
                }
            }
            var top2 = inspections.OrderByDescending(x => x).Take(2).ToArray();
            return top2[0] * top2[1];
        }

        public override object SolvePart2(string[] input)
        {
            var monkeys = ToMonkeys(input);
            var inspections = new long[monkeys.Count];
            Array.Fill(inspections, 0);
            var divisionTestProduct = 1;
            foreach (var monkey in monkeys)
            {
                divisionTestProduct *= monkey.TestDivision;
            }
            for (int round = 0; round < 10000; round++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.Items.TryDequeue(out var item))
                    {
                        inspections[monkey.Number]++;
                        item = monkey.Operation(item) % divisionTestProduct;
                        var target = (item % monkey.TestDivision == 0) ? monkey.TestTrueMonkey : monkey.TestFalseMonkey;
                        monkeys[target].Items.Enqueue(item);
                    }
                }
            }
            var top2 = inspections.OrderByDescending(x => x).Take(2).ToArray();
            return top2[0] * top2[1];
        }

        private static List<Monkey> ToMonkeys(string[] sections)
        {
            var monkeys = new List<Monkey>();
            foreach (var section in sections)
            {
                var lines = section.SplitIntoLines().Select(x => x.Trim()).ToArray();
                var number = int.Parse(lines[0].TrimEnd(':').Split(' ')[1]);
                var items = lines[1].Split(": ")[1].Split(", ").Select(long.Parse).ToArray();
                var operation = GenerateOperation(lines[2].Split(": ")[1]);
                var testDivision = int.Parse(lines[3].Split(' ')[3]);
                var testTrueMonkey = int.Parse(lines[4].Split(' ')[5]);
                var testFalseMonkey = int.Parse(lines[5].Split(' ')[5]);
                monkeys.Add(new(number, items, operation, testDivision, testTrueMonkey, testFalseMonkey));
            }
            return monkeys;
        }

        private static Func<long, long> GenerateOperation(string text)
        {
            var parts = text.Split(' ');
            if (parts[3] == "+")
            {
                var value = int.Parse(parts[4]);
                return x => x + value;
            }
            if (parts[3] == "*")
            {
                if (parts[4] == "old")
                {
                    return x => x * x;
                }
                var value = int.Parse(parts[4]);
                return x => x * value;
            }
            return x => x;
        }

        private class Monkey
        {
            public int Number { get; init; }
            public Queue<long> Items { get; init; }
            public Func<long, long> Operation { get; init; }
            public int TestDivision { get; init; }
            public int TestTrueMonkey { get; init; }
            public int TestFalseMonkey { get; init; }
            public Monkey(int number, IEnumerable<long> items, Func<long, long> operation, int testDivision, int testTrueMonkey, int testFalseMonkey)
            {
                Number = number;
                Items = new Queue<long>(items);
                Operation = operation;
                TestDivision = testDivision;
                TestTrueMonkey = testTrueMonkey;
                TestFalseMonkey = testFalseMonkey;
            }
        }
    }
}
