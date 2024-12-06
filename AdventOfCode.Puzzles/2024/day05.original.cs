namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 05, CodeType.Original)]

public class day05_original : IPuzzle
{
    public (string, string) Solve(PuzzleInput input)
    {
        var rules = input.Lines.Select(line =>
        {
            if (line.Contains('|'))
            {
                var split = line.Split("|").Select(int.Parse).ToArray();
                return (split[0], split[1]);
            }
            return (0, 0);
        }).Where(set => set != (0, 0)).ToArray();
        var manuals = input.Lines.Select(line =>
        {
            if (line.Contains(','))
            {
                return line.Split(",").Select(int.Parse).ToArray();
            }

            return null;
        }).Where(manual => manual is not null).ToArray();
        var correctManuals = new List<int[]>();
        var incorrectManuals = new List<int[]>();
        foreach (var manual in manuals)
        {
            var failed = false;
            if (manual is null) continue;
            for (var i = 0; i < manual.Length; i++)
            {
                if (failed) break;
                var reference = manual[i];
                var predicates = GetPredicates(reference, rules);
                var suffixes = GetSuffixes(reference, rules);
                for (var i2 = 0; i2 < manual.Length; i2++)
                {
                    var comparison = manual[i2];
                    if (i == i2) continue;
                    if ((i2 < i && suffixes.Contains(comparison)) || (i2 > i && predicates.Contains(comparison)))
                    {
                        failed = true;
                        break;
                    }
                }
            }
            if (!failed) correctManuals.Add(manual);
            else incorrectManuals.Add(manual);
        }

        var part1 = correctManuals.Select(manual => manual[manual.Length/2]).Sum().ToString();
        //part1 = incorrectManuals[0].ToString();

        var part2 = incorrectManuals.Select(manual =>
        {
            var sorted = SortManual(manual, rules);
            return sorted[sorted.Length / 2];
        }).Sum().ToString();

        return (part1, part2);
    }

    private static int[] SortManual(int[] manual, (int, int)[] rules)
    {
        var pagesWithRules = manual.Select(page => (page, GetPredicates(page, rules).Where(predicate => manual.Contains(predicate)).ToArray())).ToArray();
        var sortedPages = new List<int>();
        var pageZero = pagesWithRules.Where(pageAndRules => pageAndRules.Item2.Length == 0).ToArray()[0].Item1;
        while (sortedPages.Count < pagesWithRules.Length)
        {
            foreach (var pageAndRules in pagesWithRules)
            {
                if (pageAndRules.Item2.Where(predicate => manual.Contains(predicate)).Count() == 0 && !sortedPages.Contains(pageAndRules.Item1))
                {
                    manual[manual.FindIndex(item => item == pageAndRules.Item1)] = 0;
                    sortedPages.Add(pageAndRules.Item1);
                    break;
                }
            }
        }
        return sortedPages.ToArray();
    }

    private static int[] GetPredicates(int page, (int, int)[] rules)
    {
        var relatedRules = new List<int>();
        foreach (var rule in rules)
        {
            if (rule.Item2 == page) relatedRules.Add(rule.Item1);
        }
        return relatedRules.ToArray();
    }
    private static int[] GetSuffixes(int page, (int, int)[] rules)
    {
        var relatedRules = new List<int>();
        foreach (var rule in rules)
        {
            if (rule.Item1 == page) relatedRules.Add(rule.Item2);
        }
        return relatedRules.ToArray();
    }
}