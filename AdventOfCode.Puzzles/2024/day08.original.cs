namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 08, CodeType.Original)]

public class day08_original : IPuzzle
{
    public (string, string) Solve(PuzzleInput input)
    {
        var allAntennas = GetAntennas(input.Lines);
        var distinctAntennas = input.Text.Distinct().Where(item => !item.Equals('.') && !item.Equals('\n')).ToArray();
        var antennaGroupings = distinctAntennas.Select(antenna => 
            allAntennas.Where(antenna2 => antenna2.Item1.Equals(antenna)).Select(antenna2 => antenna2.Item2).ToArray()).ToArray();
        var antinodes = antennaGroupings.Select(antennas => FindAntinodes(antennas, input.Lines.Length)).ToArray();
        var part1 = antinodes.Flatten().Distinct().Count().ToString();
        var part2 = string.Empty;
        return (part1, part2);
    }

    private static (char, (int, int))[] GetAntennas(string[] input)
    {
        var antennas = new List<(char, (int, int))>();
        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] != '.') antennas.Add((input[row][col], (row, col)));
            }
        }
        return antennas.OrderBy(element => element.Item1).ToArray();
    }

    private static (int, int)[] FindAntinodes((int, int)[] antennas, int mapSize)
    {
        return  (from reference in antennas
            from comparison in antennas
            where !comparison.Equals(reference)
            select (2 * comparison.Item1 - reference.Item1, 2 * comparison.Item2 - reference.Item2))
            .Where(antinode => antinode.Item1 < mapSize && antinode.Item2 < mapSize && 
                               antinode.Item1 >= 0 && antinode.Item2 >= 0).ToArray();
    }
}