using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day8.Models;

namespace AdventOfCode.Day8;

public static class InputReader
{
    public static Input GetInput(string fileName)
    {
        var input = File.ReadAllLines($"Day8\\{fileName}");

        var nRows = input.Length;
        var nColumns = input[0].Length;

        var antennas = new List<Antenna>();
        var map = new Map(nRows, nColumns);

        for (var row = 0; row < nRows; row++)
        {
            for (var column = 0; column < nColumns; column++)
            {
                var position = new Position(row, column);
                var positionChar = input[row][column];
                if (positionChar != '.')
                {
                    antennas.Add(new Antenna(position, positionChar));
                }
                map.Fields[row, column] = new Field(new Position(row, column), positionChar);
            }
        }

        return new Input(map, antennas);
    }
}
