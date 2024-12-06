namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 05, CodeType.Original)]

public class day05_original : IPuzzle
{
    public (string, string) Solve(PuzzleInput input)
    {
        var pairs = input.Lines.Select(line =>
        {
            if (line.Contains('|'))
            {
                var split = line.Split("|").Select(int.Parse).ToArray();
                return (split[0], split[1]);
            }
            return (0, 0);
        }).Where(set => set != (0, 0)).ToArray();
        var part1 = pairs!.Count().ToString();

        var part2 = string.Empty;

        return (part1, part2);
    }
}