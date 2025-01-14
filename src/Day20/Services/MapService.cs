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
                    var isPassable = fill == '#';
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
                map.Fields[currentPosition.Row, currentPosition.Column].PicoSecondsFromStart = counter++;
            }
        }

        internal static List<Cheat> GetCheats(Map map)
        {
            var cheats = new List<Cheat>();

            var positionsFromRunWithoutCheat = map.FieldsList.Where(x=>x.PicoSecondsFromStart != null).ToList();

            foreach (var positionFromRunWithoutCheat in positionsFromRunWithoutCheat)
            {
                // get cheat positions

                // check next best position from original run

                // add cheats
            }

            return cheats;
        }

        private static Position GetNextPosition(Map map, Position currentPosition)
        {
            var directions = new List<(int, int)> { (0, 1), (0, -1), (1, 0), (-1, 0) };
            var newPosition = new Position();

            foreach (var direction in directions) 
            {
                newPosition = new Position(currentPosition.Row + direction.Item1, currentPosition.Column + direction.Item2);

                if (map.Fields[newPosition.Row,newPosition.Column].IsPassable && map.Fields[newPosition.Row, newPosition.Column].PicoSecondsFromStart is null)
                {
                    break;
                }
            }

            return newPosition;
        }
    }
}
