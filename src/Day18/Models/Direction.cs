using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day18.Models
{
    public static class Direction
    {
        public static Position Left { get; set; } = new Position(0, -1);
        public static Position Right { get; set; } = new Position(0, 1);
        public static Position Up { get; set; } = new Position(-1, 0);
        public static Position Down { get; set; } = new Position(1, 0);
        public static List<Position> List { get; set; } = new List<Position> { Left, Right, Up, Down };
    }
}
