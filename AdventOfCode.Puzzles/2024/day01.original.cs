namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 01, CodeType.Original)]
public class Day_01_Original : IPuzzle
{
	public (int, int)[] convertToArray(string[] input)
	{
		return input.Select(line =>
			{
				var balls = line.Split("   ").Select(int.Parse).ToArray();
				return (balls[0], balls[1]);
			}
		).ToArray();
	}

	public (int[], int[]) getHalves((int, int)[] input)
	{
		(int[], int[]) result = (new int[input.Length], new int[input.Length]);
		for (var y = 0; y < input.Length; y++)
		{
			result.Item1[y] = input[y].Item1;
			result.Item2[y] = input[y].Item2;
		}
		
		return result;
	}

	public int[] getDistances((int[], int[]) input)
	{
		int[] result = new int[input.Item1.Length];
		Array.Sort(input.Item1, (a, b) => a.CompareTo(b));
		Array.Sort(input.Item2, (a, b) => a.CompareTo(b));
		for (var y = 0; y < result.Length; y++)
		{
			result[y] = Math.Abs(input.Item1[y] - input.Item2[y]);
		}
		return result;
	}

	public int getSimilarity(int reference, int[] list)
	{
		return reference * list.Count(reference2 => reference2 == reference);
	}

	public int getSimilarityScore((int[], int[]) input)
	{
		int score = 0;
		for (var y = 0; y < input.Item1.Length; y++)
		{
			score += getSimilarity(input.Item1[y], input.Item2);
		}

		return score;
	}
	
	public (string, string) Solve(PuzzleInput input)
	{
		var part1 = getDistances(getHalves(convertToArray(input.Lines))).Sum().ToString();

		var part2 = getSimilarityScore(getHalves(convertToArray(input.Lines))).ToString();

		return (part1, part2);
	}
}
