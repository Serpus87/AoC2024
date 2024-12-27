using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day16.Models;

public class Field
{
    public Position Position { get; init; }
    public bool IsWall { get; init; }
    public char Fill { get; set; }
    public int? LowestScore { get; set; }

    public Field(Position position, bool isWall, char fill)
    {
        Position = position;
        IsWall = isWall;
        Fill = fill;
    }
}
