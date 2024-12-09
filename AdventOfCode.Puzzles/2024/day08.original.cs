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
        var allAntinodes = antennaGroupings.Select(antennas => FindAllAntinodes(antennas, input.Lines.Length))
            .ToArray();
        var part2 = allAntinodes.Flatten().Distinct().Count().ToString();
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

    private static (int, int)[] FindAllAntinodes((int, int)[] antennas, int mapSize)
    {
        var antinodes = new List<(int, int)>();
        foreach (var refer in antennas)
        {
            foreach (var compare in antennas)
            {
                if (refer.Equals(compare)) continue;
                var slope = (compare.Item1 - refer.Item1, compare.Item2 - refer.Item2);
                var antinode = (refer.Item1 + slope.Item1, refer.Item2 + slope.Item2);
                while (antinode.Item1 < mapSize && antinode.Item2 < mapSize &&
                       antinode.Item1 >= 0 && antinode.Item2 >= 0)
                {
                    antinodes.Add(antinode);
                    antinode = (antinode.Item1 + slope.Item1, antinode.Item2 + slope.Item2);
                }
            }
        }
        return antinodes.Distinct().ToArray();
    }
}