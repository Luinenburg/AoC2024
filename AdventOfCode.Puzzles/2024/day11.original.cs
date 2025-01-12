namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 11, CodeType.Original)]
public class Day11Original : IPuzzle
{
	private static readonly Dictionary<(uint, uint), ulong> Memoization = new ();
	public (string, string) Solve(PuzzleInput input)
	{
		var stones = input.Text.Trim().Split().Select(uint.Parse).ToArray();
		var stones25 = Blink(stones, 25);
		var stones75 = Blink(stones, 75);
		
		var part1 = stones25.ToString();

		var part2 = stones75.ToString();

		return (part1, part2);
	}

	private static ulong Blink(uint[] stones, uint maxIterations)
	{
		Memoization.Clear();
		return stones.Aggregate(0uL, (total, current) => total + BlinkHelper(current, 0, maxIterations));
	}

	private static ulong BlinkHelper(uint stone, uint iteration, uint maxIterations)
	{
		if (iteration >= maxIterations) return 1;

		var num = 0uL;
		if (Memoization.ContainsKey((stone, iteration)))
		{
			num += Memoization[(stone, iteration)];
		}
		else if (stone == 0)
		{
			num += BlinkHelper(1, iteration+1, maxIterations);
		}
		else if (stone.ToString().Length % 2 == 1)
		{
			num += BlinkHelper(stone * 2024, iteration+1, maxIterations);
		}
		else
		{
			var stoneLength = stone.ToString().Length;
			num += BlinkHelper((uint) (stone / Math.Pow(10, stoneLength/2)), iteration+1, maxIterations);
			num += BlinkHelper((uint) (stone % Math.Pow(10, stoneLength/2)), iteration+1, maxIterations);
		}

		Memoization.TryAdd((stone, iteration), num);

		return num;
	}
}