namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 02, CodeType.Original)]
public class Day02Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		var part1 = input.Lines
			.Select(line =>
				TestSafe(line.Split()
					.Select(int.Parse)
					.ToArray()))
			.Count(val => val)
			.ToString();

		var part2 = input.Lines
			.Select(line =>
				ReinforcedSafe(line.Split()
					.Select(int.Parse)
					.ToArray()))
			.Count(val => val)
			.ToString();

		return (part1, part2);
	}

	private static bool TestSafe(int[] input)
	{
		var increases = 0;
		var decreases = 0;
		for (var i = 0; i < input.Length - 1; i++)
		{
			var reference = input[i];
			var comparison = input[i + 1];
			if (reference == -1 || (comparison == -1 && i == input.Length - 2)) continue;
			if (comparison == -1) comparison = input[i + 2];
			if (Math.Abs(reference - comparison) < 1 || Math.Abs(comparison - reference) > 3) return false;
			if (reference - comparison < 0) decreases++;
			else increases++;
			if (increases > 0 && decreases > 0) return false;
		}

		return true;
	}

	private static bool ReinforcedSafe(int[] input)
	{
		if (TestSafe(input)) return true;
		var falseInput = new int[input.Length];
		for (var i = 0; i < input.Length; i++)
		{
			input.CopyTo(falseInput, 0);
			falseInput[i] = -1;
			var a = TestSafe(falseInput);
			if (TestSafe(falseInput)) return true;
		}

		return false;
	}
}