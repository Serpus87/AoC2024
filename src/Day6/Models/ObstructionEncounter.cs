using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Enums;

namespace AdventOfCode.Day6.Models;

public class ObstructionEncounter
{
    public Position Position { get; set; }
    public MoveDirection MoveDirection { get; set; }

    public ObstructionEncounter(Position position, MoveDirection moveDirection)
    {
        Position = position;
        MoveDirection = moveDirection;
    }
}
