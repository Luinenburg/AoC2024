namespace AdventOfCode.Puzzles._2024;
// [Puzzle(2024, 03, CodeType.Original)]
public class Day03Bad : IPuzzle
{
    public (string, string) Solve(PuzzleInput input)
    {
        var pattern = "(mul\\(\\d+,\\d+\\))|(do\\(\\))|(don't\\(\\))";
        Regex regex = new(pattern, RegexOptions.Compiled);
        var matches = regex.Matches(input.Text).Select(match => match.ToString()).ToArray();
        var parsed = matches.Select(match =>
        {
            if (!match.Contains("mul")) return 0;
            var nums = match.Substring(4, match.Length - 5).Split(",").Select(int.Parse).ToArray();
            return nums[0] * nums[1];
        }).ToArray();
        var part1 = parsed.Sum().ToString();
        var allowed_matches = new List<string>();
        var can_add = true;
        foreach (var match in matches)
        {
            if (match.Equals("do()"))
            {
                can_add = true;
                continue;
            }
            if (match.Equals("don't()"))
            {
                can_add = false;
                continue;
            }
            if (can_add) allowed_matches.Add(match);
        }
		
        var part2 = allowed_matches.Select(match =>
        {
            var nums = match.Substring(4, match.Length - 5).Split(",").Select(int.Parse).ToArray();
            return nums[0] * nums[1];
        }).ToArray().Sum().ToString( );
		
        return (part1, part2);
    }
	
	
}