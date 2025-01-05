namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 10, CodeType.Original)]
public class Day10Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		var positions = input.Lines.Select(line => line.Select(a => int.Parse(a.ToString())).ToArray()).ToArray();
		var unique = 0;
		var all = 0;
		for (int rows = 0; rows < positions.Length; rows++)
			for (int columns = 0; columns < positions[rows].Length; columns++)
				if (positions[rows][columns] == 0)
				{
					var trails = MovingUp(positions, (rows, columns), positions[rows][columns]);
					unique += trails.Distinct().Count();
					all += trails.Length;
				}
		var part1 = unique.ToString();

		var part2 = all.ToString();

		return (part1, part2);
	}

	public (int, int)[] MovingUp(int[][] positions, (int, int) coordinate, int currentPosition)
	{
		var peaks = new List<(int, int)>();
		(int, int)[] offsets = { (-1, 0), (1, 0), (0, -1), (0, 1) };
		foreach (var offset in offsets)
		{
			var newCoordinate = (coordinate.Item1 + offset.Item1, coordinate.Item2 + offset.Item2);
			if (newCoordinate.Item1 < 0 || newCoordinate.Item1 > positions.Length - 1 ||
			    newCoordinate.Item2 < 0 || newCoordinate.Item2 > positions.Length - 1) continue;
			var newPosition = positions[newCoordinate.Item1][newCoordinate.Item2];
			if (currentPosition == 8 && newPosition == 9) peaks.Add(newCoordinate);
			if (newPosition == currentPosition + 1 && currentPosition < 8)
				peaks.AddRange(MovingUp(positions, newCoordinate, newPosition));
		}

		return peaks.ToArray();
	}
}