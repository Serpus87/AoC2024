using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day8.Models;

public class Field
{
    public Position Position { get; init; }
    public char Fill { get; set; }
    public bool HasAntiNode { get; set; }

    public Field(Position position, char fill)
    {
        Position = position;
        Fill = fill;
        HasAntiNode = false;
    }
}
