using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day20.Models
{
    public class Cheat
    {
        public Position Start { get; set; }
        public Position End { get; set; }
        public int TimeSaved { get; set; }

        public Cheat(Position start, Position end, int timeSaved)
        {
            Start = start;
            End = end;
            TimeSaved = timeSaved;
        }
    }
}
