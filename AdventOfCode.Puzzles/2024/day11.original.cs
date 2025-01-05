using System.Collections.Concurrent;

namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 11, CodeType.Original)]
public class Day11Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		var stones = input.Text.Trim().Split().Select(long.Parse).ToArray();
		var stones25 = Blink(stones, 25);
		var stones75 = Blink(stones, 75);
		
		var part1 = stones25.ToString();

		var part2 = stones75.ToString();

		return (part1, part2);
	}

	private static long Blink(long[] stones, int maxIterations)
	{
		return stones.Aggregate(0L, (total, current) => BlinkHelper(current, 0, maxIterations));
	}

	private static long BlinkHelper(long stone, int iteration, int maxIterations)
	{
		if (iteration == maxIterations) return 1;

		var num = 0L;
		if (stone == 0)
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
			num += BlinkHelper((long) (stone / Math.Pow(10, stoneLength/2)), iteration+1, maxIterations);
			num += BlinkHelper((long) (stone % Math.Pow(10, stoneLength/2)), iteration+1, maxIterations);
		}

		return num;
	}
}