using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day16.Enums;

namespace AdventOfCode.Day16.Models;

public class Move
{
    public Position Position { get; set; }
    public DirectionEnum Direction { get; set; }

    public Move(Position position, DirectionEnum direction)
    {
        Position = position;
        Direction = direction;
    }
}
