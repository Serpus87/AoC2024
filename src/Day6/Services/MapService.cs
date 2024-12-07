using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Models;

namespace AdventOfCode.Day6.Services;

public class MapService
{
    public Map SetUpMap(string[] input)
    {
        var nRows = input.Length;
        var nColumns = input[0].Length;

        var map = new Map(nRows, nColumns);

        for (var row = 0; row < nRows; row++)
        {
            for (var column = 0; column < nColumns; column++)
            {
                map.Fields[row, column] = new Field(new Position(row,column),input[row][column]);
            }
        }

        return map;
    }

    public void Print(Map map)
    {
        Console.Clear();

        for (var row = 0;row < map.NRows; row++)
        {
            var rowToPrint = new List<char>();
            for(var column = 0;column < map.NColumns; column++)
            {
                rowToPrint.Add(map.Fields[row, column].Fill);
            }

            Console.WriteLine(string.Join(' ',rowToPrint));
        }
    }
}
