using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day10.Models;

public class Field
{
    public Position Position { get; init; }
    public int Height { get; init; }
    public bool IsPassable { get; set; } = true;

    public Field(Position position, int height)
    {
        Position = position;
        Height = height;
    }
}
