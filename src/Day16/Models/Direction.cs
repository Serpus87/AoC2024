using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day16.Enums;

namespace AdventOfCode.Day16.Models;

public static class Direction
{
    public static Move East { get; set; } = new Move(new Position(0, -1), DirectionEnum.East);
    public static Move West { get; set; } = new Move(new Position(0, 1), DirectionEnum.West);
    public static Move North { get; set; } = new Move(new Position(-1, 0), DirectionEnum.North);
    public static Move South { get; set; } = new Move(new Position(1, 0), DirectionEnum.South);
    public static List<Move> List { get; set; } = new List<Move> { East, West, North, South };
}
