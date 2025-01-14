using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day20.Models
{
    public class Field
    {
        public Position Position { get; init; }
        public char Fill { get; init; }
        public int? PicoSecondsFromStart { get; set; }
        public bool IsPassable { get; init; }

        public Field(Position position, bool isPassable, char fill)
        {
            Position = position;
            IsPassable = isPassable;
            Fill = fill;
        }
    }
}
