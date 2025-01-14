using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day20.Models;

namespace AdventOfCode.Day20.Services
{
    public static  class MapService
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

                    if (fill == 'S')
                    {
                        map.Start = position;
                    }

                    if (fill == 'E')
                    {
                        map.End = position;
                    }

                    map.Fields[row, column] = field;
                    map.FieldsList.Add(field);
                }
            }

            return map;
        }
    }
}
