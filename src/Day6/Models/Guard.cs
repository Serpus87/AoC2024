using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Enums;

namespace AdventOfCode.Day6.Models;

public class Guard
{
    public Position Position { get; set; }
    public MoveDirection MoveDirection { get; set; }
    public int MoveCounter { get; set; }

    public Guard(Position position)
    {
        Position = position;
        MoveDirection = MoveDirection.Up;
        MoveCounter = 1;
    }

    public void TurnRight90Degrees()
    {
        switch (MoveDirection)
        {
            case MoveDirection.Up: MoveDirection = MoveDirection.Right; break;
            case MoveDirection.Right: MoveDirection = MoveDirection.Down; break;
            case MoveDirection.Down: MoveDirection = MoveDirection.Left; break;
            case MoveDirection.Left: MoveDirection = MoveDirection.Up; break;
        }
    }
}
