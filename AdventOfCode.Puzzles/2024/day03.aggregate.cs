namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 03, CodeType.Revised)]

public class Day03Aggregate : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		var matches = MulDoDontRegex.Search().Matches(input.Text).Select(match => match.ToString()).ToArray();
		var part1 = matches.Aggregate(0, (total, value) => value.Contains('m') ? total + ParseMul(value) : total).ToString();

		var p2Sum = 0;
		var canAdd = true;
		foreach (var match in matches)
		{
			switch (match)
			{
				case "do()":
					canAdd = true;
					continue;
				case "don't()":
					canAdd = false;
					continue;
			}
			if (canAdd) p2Sum += ParseMul(match);
		}
		
		var part2 = p2Sum.ToString();
		
		return (part1, part2);
	}
	private static int ParseMul(string input)
	{
		return input.Substring(4, input.Length - 5).Split(',').Aggregate(1, (total, value) => total * int.Parse(value));
	}
}

public static partial class MulDoDontRegex
{
	[GeneratedRegex(@"(mul\(\d+,\d+\))|(do\(\))|(don't\(\))")]
	public static partial Regex Search();
}