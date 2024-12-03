namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 03, CodeType.Original)]
public class Day03Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		const string pattern = @"(mul\(\d+,\d+\))|(do\(\))|(don't\(\))";
		var regex = new Regex(pattern);
		var matches = regex.Matches(input.Text).Select(match => match.ToString()).ToArray();
		var part1 = matches.Select(match => match.Contains('m') ? ParseMul(match) : 0).ToArray().Sum().ToString();

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
		var a = input.Substring(4, input.Length - 5).Split(',').Select(int.Parse).ToArray();
		return a[0] * a[1];
	}
	
}