using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day6.Models;

public class Guard
{
    public Position Position { get; set; }
    //public MoveDirection MoveDirection { get; set; }

    public Guard(Position position)
    {
        Position = position;
        //MoveDirection = MoveDirection.Up;
    }
}
