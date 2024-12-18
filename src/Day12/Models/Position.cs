using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day12.Models;

public class Position
{
    public int Row { get; init; }
    public int Column { get; init; }

    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }
}
