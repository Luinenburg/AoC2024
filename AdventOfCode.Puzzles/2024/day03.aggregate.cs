namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 03, CodeType.Revised)]
public class Day03Aggregate : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		var matches = MulDoDontRegex.Search().Matches(input.Text).Aggregate((true, 0, 0), (running, match) =>
			match.ValueSpan switch
			{
				"do()" => (true, running.Item2, running.Item3),
				"don't()" => (false, running.Item2, running.Item3),
				_ when running.Item1 => (true, running.Item2 + ParseMul(match.Value),
					running.Item3 + ParseMul(match.Value)),
				_ when !running.Item1 => (false, running.Item2 + ParseMul(match.Value), running.Item3),
				_ => running
			}
		);
		var part1 = matches.Item2.ToString();

		var part2 = matches.Item3.ToString();

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