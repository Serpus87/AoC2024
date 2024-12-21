using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day14.Models;

public class Robot
{
    public Location Position { get; set; }
    public Location Velocity { get; init; }

    public Robot(Location position, Location velocity)
    {
        Position = position;
        Velocity = velocity;
    }
}
