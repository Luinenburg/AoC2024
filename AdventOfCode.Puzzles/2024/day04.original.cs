namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 04, CodeType.Original)]
public class Day04Original : IPuzzle
{
	private static bool checkAxis((int, int) pivot, string[] input, (char, int) reference, (int, int) direction)
	{
		var comparison = reference switch
		{
			('X', 0) => ('M', reference.Item2 + 1),
			('M', 1) => ('A', reference.Item2 + 1),
			('A', 2) => ('S', reference.Item2 + 1),
			('S', 3) => ('J', reference.Item2 + 1),
			_ => ('_', -1)
		};
		if (comparison.Item2 == 4) return true;
		if (comparison.Item2 == -1) return false;

		if (pivot.Item1 + direction.Item1 < 0 || pivot.Item1 + direction.Item1 >= input[pivot.Item2].Length || pivot.Item2 + direction.Item2 < 0 || pivot.Item2 + direction.Item2 >= input.Length) return false;
		var test = input[pivot.Item2 + direction.Item2][pivot.Item1 + direction.Item1];
		if (test.Equals(comparison.Item1)) return checkAxis((pivot.Item1 + direction.Item1, pivot.Item2 + direction.Item2), input, comparison, direction);
		
		return false;
	}

	private static bool checkSurroundings((int, int) pivot, string[] input, char reference)
	{
		var comparison = reference switch
		{
			'X' => 'M',
			'M' => 'A',
			'A' => 'S',
			_ => '_'
		};
		if (comparison.Equals('_')) return true;
		for (var y = -1; y < 2; y += 1)
		{
			for (var x = -1; x < 2; x += 1)
			{
				if (x == 0 && y == 0) continue;
				if (checkAxis(pivot, input, (reference, 0), (x, y))) return true;
			}
		}

		return false;
	}
	public (string, string) Solve(PuzzleInput input)
	{
		int sum = 0;
		for (int y = 0; y < input.Lines.Length; y++)
		{
			for (int x = 0; x < input.Lines[y].Length; x++)
			{
				if (input.Lines[y][x] == 'X')
				{
					if (checkSurroundings((x, y), input.Lines, 'X')) sum += 1;
				}
			}
		}
		var part1 = sum.ToString();

		var part2 = string.Empty;

		return (part1, part2);
	}
}