using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day20.Extensions;
using AdventOfCode.Day20.Models;

namespace AdventOfCode.Day20.Services
{
    public static class MapService
    {
        public static Map GetMap(string[] input)
        {
            var nRows = input.Length;
            var nColumns = input[0].Length;

            var map = new Map(nRows, nColumns);

            for (var row = 0; row < nRows; row++)
            {
                for (var column = 0; column < nColumns; column++)
                {
                    var fill = input[row][column];
                    var isPassable = fill != '#';
                    var position = new Position(row, column);
                    var field = new Field(position, isPassable, fill);

                    map.Fields[row, column] = field;
                    map.FieldsList.Add(field);

                    if (fill == 'S')
                    {
                        map.Fields[row, column].PicoSecondsFromStart = 0;
                        map.Start = position;
                    }

                    if (fill == 'E')
                    {
                        map.End = position;
                    }
                }
            }

            return map;
        }

        public static void RunWithoutCheating(Map map)
        {
            var currentPosition = map.Start;
            var counter = 0;

            while (!currentPosition.IsEqual(map.End))
            {
                currentPosition = GetNextPosition(map, currentPosition);
                map.Fields[currentPosition.Row, currentPosition.Column].PicoSecondsFromStart = ++counter;
            }
        }

        public static List<Cheat> GetCheats(Map map)
        {
            var cheats = new List<Cheat>();

            var positionsFromRunWithoutCheat = map.FieldsList.Where(x => x.PicoSecondsFromStart != null).Select(x => x.Position).ToList();

            foreach (var positionFromRunWithoutCheat in positionsFromRunWithoutCheat)
            {
                // get cheat positions
                var cheatsFromPosition = GetCheatsFromPosition(map, positionFromRunWithoutCheat);

                // add cheats
                cheats.AddRange(cheatsFromPosition);
            }

            return cheats;
        }

        private static List<Cheat> GetCheatsFromPosition(Map map, Position positionFromRunWithoutCheat)
        {
            var cheatsFromPosition = new List<Cheat>();
            var picoSecondsFromStart = (int)map.Fields[positionFromRunWithoutCheat.Row, positionFromRunWithoutCheat.Column].PicoSecondsFromStart!;

            var cheatStartPositions = GetCheatStartPositions(map, positionFromRunWithoutCheat);

            foreach (var cheatStartPosition in cheatStartPositions)
            {
                //var cheatEndPositions = GetCheatEndPositions(map, cheatStartPosition, picoSecondsFromStart);

                //foreach (var cheatEndPosition in cheatEndPositions)
                //{
                //    var picoSecondsFromStartAtCheatEndPosition = (int)map.Fields[cheatEndPosition.Row, cheatEndPosition.Column].PicoSecondsFromStart!;
                //    var timeSaved = picoSecondsFromStartAtCheatEndPosition - picoSecondsFromStart + 2;

                //    cheatsFromPosition.Add(new Cheat(positionFromRunWithoutCheat, cheatStartPosition, cheatEndPosition, timeSaved));
                //}

                var cheatEndPosition = GetCheatEndPosition(map, cheatStartPosition, picoSecondsFromStart);

                if (cheatEndPosition != null) 
                {
                    var picoSecondsFromStartAtCheatEndPosition = (int)map.Fields[cheatEndPosition.Row, cheatEndPosition.Column].PicoSecondsFromStart!;
                    var timeSaved = picoSecondsFromStartAtCheatEndPosition - picoSecondsFromStart + 2;

                    cheatsFromPosition.Add(new Cheat(positionFromRunWithoutCheat, cheatStartPosition, cheatEndPosition, timeSaved));
                }
            }

            return cheatsFromPosition;
        }

        private static Position? GetCheatEndPosition(Map map, Position cheatStartPosition, int picoSecondsFromStart)
        {
            var cheatEndPosition = new Position();
            var directions = new List<(int, int)> { (0, 1), (0, -1), (1, 0), (-1, 0) };
            var mostTimeSaved = 0;

            foreach (var direction in directions)
            {
                var newRow = cheatStartPosition.Row + direction.Item1;
                var newColumn = cheatStartPosition.Column + direction.Item2;

                if (newRow < 0 || newRow >= map.NumberOfRows || newColumn < 0 || newColumn >= map.NumberOfColumns)
                {
                    continue;
                }

                var newPosition = new Position(newRow, newColumn);

                if (map.Fields[newPosition.Row, newPosition.Column].IsPassable)
                {
                    var timeSaved = (int)map.Fields[newPosition.Row, newPosition.Column].PicoSecondsFromStart! - (picoSecondsFromStart + 2);
                    if (timeSaved > mostTimeSaved) 
                    {
                        mostTimeSaved = timeSaved;
                        cheatEndPosition = newPosition;
                    }
                }
            }

            if (!map.Fields[cheatEndPosition.Row, cheatEndPosition.Column].IsPassable)
            {
                return null;
            }

            return cheatEndPosition;
        }

        private static List<Position> GetCheatEndPositions(Map map, Position cheatStartPosition, int? picoSecondsFromStart)
        {
            var cheatEndPositions = new List<Position>();

            var directions = new List<(int, int)> { (0, 1), (0, -1), (1, 0), (-1, 0) };
            var newPosition = new Position();

            foreach (var direction in directions)
            {
                var newRow = cheatStartPosition.Row + direction.Item1;
                var newColumn = cheatStartPosition.Column + direction.Item2;

                if (newRow < 0 || newRow >= map.NumberOfRows || newColumn < 0 || newColumn >= map.NumberOfColumns)
                {
                    continue;
                }

                newPosition = new Position(newRow, newColumn);

                if (map.Fields[newPosition.Row, newPosition.Column].IsPassable && map.Fields[newPosition.Row, newPosition.Column].PicoSecondsFromStart > (picoSecondsFromStart + 2))
                {
                    cheatEndPositions.Add(newPosition);
                }
            }

            return cheatEndPositions;
        }

        private static List<Position> GetCheatStartPositions(Map map, Position positionFromRunWithoutCheat)
        {
            var cheatStartPositions = new List<Position>();

            var directions = new List<(int, int)> { (0, 1), (0, -1), (1, 0), (-1, 0) };
            var newPosition = new Position();

            foreach (var direction in directions)
            {
                newPosition = new Position(positionFromRunWithoutCheat.Row + direction.Item1, positionFromRunWithoutCheat.Column + direction.Item2);

                if (!map.Fields[newPosition.Row, newPosition.Column].IsPassable)
                {
                    cheatStartPositions.Add(newPosition);
                }
            }

            return cheatStartPositions;
        }

        private static Position GetNextPosition(Map map, Position currentPosition)
        {
            var directions = new List<(int, int)> { (0, 1), (0, -1), (1, 0), (-1, 0) };
            var newPosition = new Position();

            foreach (var direction in directions)
            {
                newPosition = new Position(currentPosition.Row + direction.Item1, currentPosition.Column + direction.Item2);

                if (map.Fields[newPosition.Row, newPosition.Column].IsPassable && map.Fields[newPosition.Row, newPosition.Column].PicoSecondsFromStart is null)
                {
                    break;
                }
            }

            return newPosition;
        }
    }
}
