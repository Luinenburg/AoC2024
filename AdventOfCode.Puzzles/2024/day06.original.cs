namespace AdventOfCode.Puzzles._2024;

[Puzzle(2024, 06, CodeType.Original)]

public class day06_original : IPuzzle
{
    public (string, string) Solve(PuzzleInput input)
    {
        var guard = new Guard(FindGuard(input.Lines), (-1, 0));
        var originalGuard = new Guard(FindGuard(input.Lines), (-1, 0));;
        var visitedPositionsList = new List<(int, int)>();
        while (true)
        {
            var oldPos = guard.Position;
            guard = MoveGuard(guard, input.Lines, (-1, -1));
            if (oldPos != guard.Position) visitedPositionsList.Add(guard.Position);
            if (guard.Position.Item1 >= input.Lines.Length-1 || guard.Position.Item2 >= input.Lines.Length-1 || guard.Position.Item1 <= 0 || guard.Position.Item2 <= 0) break;
        }

        var visitedPositions = visitedPositionsList.ToArray();
        var part1 = visitedPositions.Distinct()
            .Count()
            .ToString();

        var part2 = ValidObstaclePlacement(originalGuard, visitedPositions.Distinct().ToArray(), input.Lines).Length.ToString();
        return (part1, part2);
    }

    private static (int, int) FindGuard(string[] input)
    {
        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] == '^') return (row, col);
            }
        }

        return (0, 0);
    }

    private static Guard MoveGuard(Guard guard, string[] input, (int, int) blocker)
    {
        var nextPosition = (guard.Position.Item1 + guard.Direction.Item1, guard.Position.Item2 + guard.Direction.Item2);
        var nextObject = input[nextPosition.Item1][nextPosition.Item2];
        if (nextObject == '#' || nextPosition.Equals(blocker))
        {
            guard.Direction = guard.Direction switch
            {
                (-1, 0) => (0, 1),
                (0, 1) => (1, 0),
                (1, 0) => (0, -1),
                (0, -1) => (-1, 0),
                _ => (0, 0)
            };
            
        } else guard.Position = nextPosition;

        return guard;
    }

    private static (int, int)[] ValidObstaclePlacement(Guard originalGuard, (int, int)[] visitedPositions, string[] input)
    {
        var validObstaclePositions = new List<(int, int)>();
        foreach (var position in visitedPositions)
        {
            if (position == originalGuard.Position) continue;
            Guard guard = new Guard(originalGuard.Position, originalGuard.Direction);
            var visitedPositionsList = new List<((int, int), (int, int))>();
            while (true)
            {
                var oldPos = guard.Position;
                guard = MoveGuard(guard, input, position);
                if (visitedPositionsList.Contains((guard.Position, guard.Direction)))
                {
                    validObstaclePositions.Add(guard.Position);
                    break;
                }
                if (oldPos != guard.Position) visitedPositionsList.Add((guard.Position, guard.Direction));
                if (guard.Position.Item1 >= input.Length-1 || guard.Position.Item2 >= input.Length-1 || guard.Position.Item1 <= 0 || guard.Position.Item2 <= 0) break;
            }
        }
        return validObstaclePositions.ToArray();
    }
}

public class Guard ((int, int) position, (int, int) direction)
{
    public (int, int) Position { get; set; } = position;
    public (int, int) Direction { get; set; } = direction;
}