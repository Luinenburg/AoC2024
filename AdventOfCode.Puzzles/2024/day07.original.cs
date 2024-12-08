namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 07, CodeType.Original)]

public class day07_original : IPuzzle
{
    enum Operation
    {
        Add,
        Multiply,
        Join
    };
    public (string, string) Solve(PuzzleInput input)
    {
        var nums = input.Lines.Select(line =>
        {
            var halves = line.Split(':');
            var secondHalf = halves[1].Trim().Split(' ').Select(long.Parse).ToArray();
            return (long.Parse(halves[0]), secondHalf);
        }).ToArray();
        var calibrated_nums = nums.Select(num => TestCalibration(num)).ToArray();
        var part1 = calibrated_nums.Sum().ToString();

        var calibrated_nums2 = nums.Select(num => TestCalibration2(num)).ToArray();
        var part2 = calibrated_nums2.Sum().ToString();
        return (part1, part2);
    }

    private static long TestCalibration((long, long[]) input)
    {
        var calibration = input.Item1;
        var nums = input.Item2[1..];
        var numsAndOperations = nums.Select(num => (num, Operation.Add)).ToArray();
        var sum = input.Item2[0];
        while (true)
        {
            sum = input.Item2[0];
            foreach (var (num, operation) in numsAndOperations)
            {
                switch (operation)
                {
                    case Operation.Add:
                        sum += num;
                        continue;
                    case Operation.Multiply:
                        sum *= num;
                        continue;
                    default:
                        return -1;
                }
            }

            if (sum == calibration) return calibration;
            if (numsAndOperations.Count(combined => combined.ToTuple().Item2.Equals(Operation.Multiply)) == numsAndOperations.Length) break;
            numsAndOperations = changeOperations(numsAndOperations);
        }

        return 0;
    }

    private static (long, Operation)[] changeOperations((long, Operation)[] input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            var escape_flag = false;
            switch (input[i].Item2)
            {
                case Operation.Add:
                    input[i].Item2 = Operation.Multiply;
                    escape_flag = true;
                    break;
                case Operation.Multiply:
                    input[i].Item2 = Operation.Add;
                    break;
            }

            if (escape_flag) break;
        }

        return input;
    }
    
    private static long TestCalibration2((long, long[]) input)
    {
        var calibration = input.Item1;
        var nums = input.Item2[1..];
        var numsAndOperations = nums.Select(num => (num, Operation.Add)).ToArray();
        var sum = input.Item2[0];
        while (true)
        {
            sum = input.Item2[0];
            foreach (var (num, operation) in numsAndOperations)
            {
                switch (operation)
                {
                    case Operation.Add:
                        sum += num;
                        continue;
                    case Operation.Multiply:
                        sum *= num;
                        continue;
                    case Operation.Join:
                        var digits = num.ToString().Length;
                        sum = sum * (long) Math.Pow(10, digits) + num;
                        continue;
                    default:
                        return -1;
                }
            }

            if (sum == calibration) return calibration;
            if (numsAndOperations.Count(combined => combined.ToTuple().Item2.Equals(Operation.Join)) == numsAndOperations.Length) break;
            numsAndOperations = changeOperations2(numsAndOperations);
        }

        return 0;
    }
    private static (long, Operation)[] changeOperations2((long, Operation)[] input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            var escape_flag = false;
            switch (input[i].Item2)
            {
                case Operation.Add:
                    input[i].Item2 = Operation.Multiply;
                    escape_flag = true;
                    break;
                case Operation.Multiply:
                    input[i].Item2 = Operation.Join;
                    escape_flag = true;
                    break;
                case Operation.Join:
                    input[i].Item2 = Operation.Add;
                    break;
            }

            if (escape_flag) break;
        }

        return input;
    }
}