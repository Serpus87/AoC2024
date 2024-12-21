using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day14.Models;

public class Field
{
    public Location Position { get; set; }
    public int NumberOfRobots { get; set; }

    public Field(Location position, int numberOfRobots)
    {
        Position = position;
        NumberOfRobots = numberOfRobots;
    }
}
