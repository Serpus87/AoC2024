using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day16.Enums;
using AdventOfCode.Day16.Models;

namespace AdventOfCode.Day16;

public static class MazeService
{
    public static Maze GetMaze(string[] input)
    {
        var nRows = input.Length;
        var nColumns = input[0].Length;

        var maze = new Maze(nRows, nColumns);

        for (var row = 0; row < nRows; row++)
        {
            for (var column = 0; column < nColumns; column++)
            {
                var fill = input[row][column];
                var isWall = fill == '#';
                maze.Fields[row, column] = new Field(new Position(row, column), isWall, fill);

                if (fill == 'S')
                {
                    maze.Start = new Position(row, column);
                }
                if (fill == 'E')
                {
                    maze.End = new Position(row, column);
                }
            }
        }

        return maze;
    }

    public static void Print(Maze maze)
    {
        Console.Clear();

        for (var row = 0; row < maze.NRows; row++)
        {
            var rowToPrint = new List<string>();
            for (var column = 0; column < maze.NColumns; column++)
            {
                rowToPrint.Add(maze.Fields[row, column].Fill.ToString());
            }

            Console.WriteLine(string.Join(' ', rowToPrint));
        }
    }

    public static List<Reindeer> GetReindeersWithShortestRoute(Maze maze)
    {
        var areReindeersDoneWalking = false;
        var firstReindeer = new Reindeer(maze.Start);
        var reindeers = new List<Reindeer> { firstReindeer };

        while (!areReindeersDoneWalking)
        {
            var reindeersThatWillWalk = reindeers.Where(x => !x.IsDoneWalking).ToList();

            foreach (var reindeer in reindeersThatWillWalk)
            {
                reindeer.PreviousPositions.Add(reindeer.Position);
                var previousPosition = new Position(reindeer.Position.Row, reindeer.Position.Column);
                var availableMoves = GetAvailableMoves(maze, reindeers, reindeer);

                if (availableMoves.Count == 0)
                {
                    reindeer.IsDoneWalking = true;
                    continue;
                }

                for (var i = 1; i < availableMoves.Count; i++)
                {
                    var newReindeer = new Reindeer(reindeer);

                    reindeers.Add(newReindeer);
                    newReindeer.MakeMove(availableMoves[i]);

                    if (maze.Fields[previousPosition.Row, previousPosition.Column].LowestScore == null || (maze.Fields[previousPosition.Row, previousPosition.Column].LowestScore + 1000) >= reindeer.Score)
                    {
                        maze.Fields[previousPosition.Row, previousPosition.Column].LowestScore = newReindeer.Score;
                    }
                    else
                    {
                        reindeer.IsDoneWalking = true;
                    }

                    if (newReindeer.Position.Row == maze.End.Row && newReindeer.Position.Column == maze.End.Column)
                    {
                        reindeer.PreviousPositions.Add(newReindeer.Position);
                        newReindeer.IsDoneWalking = true;
                    }
                }

                reindeer.MakeMove(availableMoves.First());

                if (maze.Fields[previousPosition.Row, previousPosition.Column].LowestScore == null || (maze.Fields[previousPosition.Row, previousPosition.Column].LowestScore + 1000) >= reindeer.Score)
                {
                    maze.Fields[previousPosition.Row, previousPosition.Column].LowestScore = reindeer.Score;
                }
                else
                {
                    reindeer.IsDoneWalking = true;
                }

                if (reindeer.Position.Row == maze.End.Row && reindeer.Position.Column == maze.End.Column)
                {
                    reindeer.PreviousPositions.Add(reindeer.Position);
                    reindeer.IsDoneWalking = true;
                }
            }

            areReindeersDoneWalking = reindeers.All(x => x.IsDoneWalking);
        }

        var reindeersThatFoundEnd = reindeers.Where(x => x.Position.Row == maze.End.Row && x.Position.Column == maze.End.Column).ToList();

        return reindeersThatFoundEnd;
    }

    private static List<Move> GetAvailableMoves(Maze maze, List<Reindeer> reindeers, Reindeer reindeer)
    {
        var availablePositions = new List<Move>();
        var directions = Direction.List;

        foreach (var direction in directions)
        {
            var newPosition = new Position(reindeer.Position.Row + direction.Position.Row, reindeer.Position.Column + direction.Position.Column);
            var isNewPositionAvailable = !maze.Fields[newPosition.Row, newPosition.Column].IsWall;
            var hasCurrentPositionHigherScore = maze.Fields[reindeer.Position.Row, reindeer.Position.Column].LowestScore == null || (maze.Fields[reindeer.Position.Row, reindeer.Position.Column].LowestScore + 1000) >= reindeer.Score;
            var hasReindeerNotBeenOnPositionBefore = !reindeer.PreviousPositions.Any(x => x.Row == newPosition.Row && x.Column == newPosition.Column);

            if (isNewPositionAvailable && hasCurrentPositionHigherScore && hasReindeerNotBeenOnPositionBefore)
            {
                availablePositions.Add(direction);
            }
        }

        return availablePositions;
    }
}
