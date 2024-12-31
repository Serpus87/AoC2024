using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day18.Models;
using Microsoft.Win32;

namespace AdventOfCode.Day18.Services
{
    public class MapService
    {
        public static List<Position> GetCorruptedLocations(string[] input)
        {
            var corruptedLocations = new List<Position>();

            foreach (var line in input)
            {
                var firstValue = line.Substring(0, line.IndexOf(','));
                var secondValue = line.Substring(line.IndexOf(',') + 1);

                var row = int.Parse(secondValue);
                var column = int.Parse(firstValue);

                corruptedLocations.Add(new Position(row, column));
            }

            return corruptedLocations;
        }

        public static void InitializeMap(Map map, List<Position> corruptedLocations)
        {
            for (var row = 0; row < map.NRows; row++)
            {
                for (var column = 0; column < map.NColumns; column++)
                {
                    var position = new Position(row, column);
                    var isCorrupted = corruptedLocations.Any(x => x.Row == position.Row && x.Column == position.Column);
                    var fill = isCorrupted ? '#' : '.';
                    var field = new Field(position, isCorrupted, fill);

                    map.Fields[position.Row, position.Column] = field;
                    map.FieldsList.Add(field);
                }
            }
        }

        public static List<PathFinder> FindShortestPaths(Map map)
        {
            var arePathFindersDoneWalking = false;
            var firstPathFinder = new PathFinder(map.Start);
            var pathFinders = new List<PathFinder> { firstPathFinder };
            var pathFindersThatFoundEnd = pathFinders.Where(x => x.Position.Row == map.End.Row && x.Position.Column == map.End.Column).ToList();

            while (!arePathFindersDoneWalking)
            {
                var pathFindersThatWillWalk = pathFinders.Where(x => !x.IsDoneWalking).ToList();

                foreach (var pathFinder in pathFindersThatWillWalk)
                {
                    pathFinder.PreviousPositions.Add(pathFinder.Position);
                    var previousPosition = new Position(pathFinder.Position.Row, pathFinder.Position.Column);
                    var availablePositions = GetAvailablePositions(map, pathFinders, pathFinder);

                    if (availablePositions.Count == 0)
                    {
                        pathFinder.IsDoneWalking = true;
                        continue;
                    }

                    for (var i = 1; i < availablePositions.Count; i++)
                    {
                        var newPathFinder = new PathFinder(pathFinder);

                        pathFinders.Add(newPathFinder);
                        newPathFinder.GoToNewPosition(availablePositions[i]);

                        if (map.Fields[previousPosition.Row, previousPosition.Column].LowestNumberOfMoves == null || map.Fields[previousPosition.Row, previousPosition.Column].LowestNumberOfMoves >= pathFinder.MoveCounter)
                        {
                            map.Fields[previousPosition.Row, previousPosition.Column].LowestNumberOfMoves = newPathFinder.MoveCounter;
                        }
                        else
                        {
                            pathFinder.IsDoneWalking = true;
                        }

                        if (newPathFinder.Position.Row == map.End.Row && newPathFinder.Position.Column == map.End.Column)
                        {
                            pathFinder.PreviousPositions.Add(newPathFinder.Position);
                            newPathFinder.IsDoneWalking = true;
                            pathFindersThatFoundEnd.Add(newPathFinder);
                            return pathFindersThatFoundEnd;
                        }
                    }

                    pathFinder.GoToNewPosition(availablePositions.First());

                    if (map.Fields[previousPosition.Row, previousPosition.Column].LowestNumberOfMoves == null || map.Fields[previousPosition.Row, previousPosition.Column].LowestNumberOfMoves >= pathFinder.MoveCounter)
                    {
                        map.Fields[previousPosition.Row, previousPosition.Column].LowestNumberOfMoves = pathFinder.MoveCounter;
                    }
                    else
                    {
                        pathFinder.IsDoneWalking = true;
                    }

                    if (pathFinder.Position.Row == map.End.Row && pathFinder.Position.Column == map.End.Column)
                    {
                        pathFinder.PreviousPositions.Add(pathFinder.Position);
                        pathFinder.IsDoneWalking = true;
                        pathFindersThatFoundEnd.Add(pathFinder);
                        return pathFindersThatFoundEnd;
                    }
                }

                arePathFindersDoneWalking = pathFinders.All(x => x.IsDoneWalking);
            }

            pathFindersThatFoundEnd = pathFinders.Where(x => x.Position.Row == map.End.Row && x.Position.Column == map.End.Column).ToList();

            return pathFindersThatFoundEnd;
        }

        private static List<Position> GetAvailablePositions(Map map, List<PathFinder> pathfinders, PathFinder pathfinder)
        {
            var availablePositions = new List<Position>();
            var directions = Direction.List;

            foreach (var direction in directions)
            {
                var newPosition = new Position(pathfinder.Position.Row + direction.Row, pathfinder.Position.Column + direction.Column);

                if (newPosition.Row < 0 || newPosition.Row >= map.NRows || newPosition.Column < 0 || newPosition.Column >= map.NColumns)
                {
                    continue;
                }

                if (pathfinders.Any(x => x.Position.Row == newPosition.Row && x.Position.Column == newPosition.Column))
                {
                    continue;
                }

                var isNewPositionAvailable = !map.Fields[newPosition.Row, newPosition.Column].IsCorrupted;
                var hasCurrentPositionHigherScore = map.Fields[pathfinder.Position.Row, pathfinder.Position.Column].LowestNumberOfMoves == null || map.Fields[pathfinder.Position.Row, pathfinder.Position.Column].LowestNumberOfMoves >= pathfinder.MoveCounter;
                var hasPathFinderNotBeenOnPositionBefore = !pathfinder.PreviousPositions.Any(x => x.Row == newPosition.Row && x.Column == newPosition.Column);

                if (isNewPositionAvailable && hasCurrentPositionHigherScore && hasPathFinderNotBeenOnPositionBefore)
                {
                    availablePositions.Add(newPosition);
                }
            }

            return availablePositions;
        }

        // todo improve this, by not walking but checking linked corruption
        public static Position FindFirstCorruptedLocationThatBlocksPath(Map map, List<Position> corruptedPositions)
        {
            var positionToCorrupt = new Position(-1, -1);
            var isResultFound = false;
            var corruptionCounter = 0;

            while (!isResultFound)
            {
                map.ResetFields();
                positionToCorrupt = corruptedPositions[corruptionCounter];

                map.Fields[positionToCorrupt.Row, positionToCorrupt.Column].IsCorrupted = true;
                map.Fields[positionToCorrupt.Row, positionToCorrupt.Column].Fill = '#';

                var pathFindersThatFoundEnd = FindShortestPaths(map);

                isResultFound = pathFindersThatFoundEnd.Count == 0;

                corruptionCounter++;
            }

            return positionToCorrupt;
        }
    }
}
