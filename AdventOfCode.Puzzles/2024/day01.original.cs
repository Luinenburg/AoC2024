namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 01, CodeType.Original)]
public class Day01Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		var part1 = GetDistances(
			GetHalves(
				ConvertToArray(input.Lines)))
			.Sum()
			.ToString();

		var part2 = GetSimilarityScore(
			GetHalves(
				ConvertToArray(input.Lines)))
			.ToString();

		return (part1, part2);
	}

	private static (int, int)[] ConvertToArray(string[] input)
	{
		return input.Select(line =>
			{
				var halves = line.Split("   ").Select(int.Parse).ToArray();
				return (halves[0], halves[1]);
			}
		).ToArray();
	}

	private static (int[], int[]) GetHalves((int, int)[] input)
	{
		var result = (new int[input.Length], new int[input.Length]);
		for (var y = 0; y < input.Length; y++)
		{
			result.Item1[y] = input[y].Item1;
			result.Item2[y] = input[y].Item2;
		}

		return result;
	}

	private static int[] GetDistances((int[], int[]) input)
	{
		var result = new int[input.Item1.Length];
		Array.Sort(input.Item1, (a, b) => a.CompareTo(b));
		Array.Sort(input.Item2, (a, b) => a.CompareTo(b));
		for (var y = 0; y < result.Length; y++)
		{
			result[y] = Math.Abs(input.Item1[y] - input.Item2[y]);
		}

		return result;
	}

	private static int GetSimilarity(int reference, int[] list)
	{
		return reference * list.Count(reference2 => reference2 == reference);
	}

	private static int GetSimilarityScore((int[], int[]) input)
	{
		var score = 0;
		foreach (var item in input.Item1)
		{
			score += GetSimilarity(item, input.Item2);
		}

		return score;
	}
}
