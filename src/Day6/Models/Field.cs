using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day6.Models;

public class Field
{
    public Position Position { get; init; }
    public char Fill { get; set; }
    public bool HasObstruction { get; init; }

    public Field(Position position, char fill)
    {
        Position = position;
        Fill = fill;
        HasObstruction = false;
    }
}
