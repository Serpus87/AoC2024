using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15.Models;

public class Field
{
    public Position Location { get; init; }
    public char Fill { get; set; }
    public bool IsWall { get; set; }

    public Field(Position location, char fill, bool isWall)
    {
        Location = location;
        Fill = fill;
        IsWall = isWall;
    }
}
