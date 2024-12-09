using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day8.Models;

public class Antenna
{
    public Position Position { get; set; }
    public char Frequency { get; set; }

    public Antenna(Position position, char frequency)
    {
        Position = position;
        Frequency = frequency;
    }
}
