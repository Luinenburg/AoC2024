namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 09, CodeType.Original)]

public class day09_original : IPuzzle
{
    public (string, string) Solve(PuzzleInput input)
    {
        var nums = input.Text.Trim().Select(a => long.Parse(a.ToString())).ToArray();
        var fileSymbol = nums.Select((num, index) => index % 2 == 0 ? (num, index/2) : (num, -1)).ToArray();
        var fileSpace = GenerateFileSpace(fileSymbol);
        //var correctedFileSpace = CorrectFileSpace(fileSpace);
        //var checksum = correctedFileSpace.Select((val, index) => val * index).Sum();
        var correctedFileSpace2 = CorrectFileSpaceBlocks(fileSpace);
        var checksum2 = correctedFileSpace2.Select((val, index) =>
        {
            var prod = val * index;
            if (prod > 0) return prod;
            return 0;
        }).Sum();
        var checksum = 0;
        var part1 = checksum.ToString();
        var part2 = checksum2.ToString();
        // bad = 6881566893085 (too high)
        return (part1, part2);
    }

    private static long[] GenerateFileSpace((long, int)[] fileSymbol)
    {
        var fileSpace = new List<long>();
        foreach (var file in fileSymbol)
        {
            for (var i = 0; i < file.Item1; i++) fileSpace.Add(file.Item2);
        }
        return fileSpace.ToArray();
    }

    private static long[] CorrectFileSpace(long[] fileSpaceOriginal)
    {
        var fileSpace = fileSpaceOriginal.Clone() as long[];
        for (var i = 0; i < fileSpace.Length; i++)
        {
            if (fileSpace[i] != -1) continue;
            for (var j = fileSpace.Length - 1; j >= 0; j--)
            {
                if (fileSpace[j] == -1) continue;
                fileSpace[i] = fileSpace[j];
                fileSpace[j] = -1;
                break;
            }
        }
        return fileSpace.Where(val => val != -1).ToArray();
    }

    private static long[] CorrectFileSpaceBlocks(long[] fileSpaceOriginal)
    {
        var fileSpace = fileSpaceOriginal.Clone() as long[];
        for (var fileId = fileSpace.Max(); fileId >= 0; fileId--)
        {
            long fileSize = fileSpace.Count(x => x == fileId);
            for (var i = 0L; i < fileSpace.IndexOf(fileId); i++)
            {
                var blankSearch = fileSpace[i];
                if (blankSearch != -1) continue;
                var blankSize = BlankSpaceLength(i, fileSpace);
                if (blankSize < fileSize)
                {
                    i += blankSize;
                    continue;
                }
                fileSpace = fileSpace.Select(x => x == fileId ? -1 : x).ToArray();
                while (fileSize != 0)
                {
                    fileSpace[i] = fileId;
                    i++;
                    fileSize--;
                }

                break;
            }
        }
        return fileSpace.Select(x => x == -1 ? -1 : x).ToArray();
    }

    private static long BlankSpaceLength(long index, long[] fileSpace)
    {
        long blankSpaceLength = 0;
        while (fileSpace[index] == -1)
        {
            index++;
            blankSpaceLength++;
            if (index >= fileSpace.Length) break;
        }
        return blankSpaceLength;
    }

    private static long DataSpaceLength(long index, long[] fileSpace)
    {
        long dataSpaceLength = 0;
        while (fileSpace[index] != -1)
        {
            index--;
            dataSpaceLength++;
            if (index < 0) break;
        }
        return dataSpaceLength;
    }
}