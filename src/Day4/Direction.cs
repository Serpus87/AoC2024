using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4;

public class Direction
{
    public int HorizontalDirection { get;set; }
    public int VerticalDirection { get;set; }

    public Direction(int horizontalDirection, int verticalDirection)
    {
        HorizontalDirection = horizontalDirection;
        VerticalDirection = verticalDirection;
    }
}
