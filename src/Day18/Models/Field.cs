using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day18.Models
{
    public class Field
    {
        public Position Position { get; set; }
        public char Fill { get; set; } = '.';
        public bool IsCorrupted { get; set; } = false;
        public int? LowestNumberOfMoves { get; set; }

        public Field(Position position, bool isCorrupted, char fill)
        {
            Position = position;
            IsCorrupted = isCorrupted;
            Fill = fill;
        }
    }
}
