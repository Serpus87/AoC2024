using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15.Models;

public class Move
{
    public int Vertical { get; set; }
    public int Horizontal { get; set; }

    public Move(int vertical, int horizontal)
    {
        Vertical = vertical;
        Horizontal = horizontal;
    }
}
