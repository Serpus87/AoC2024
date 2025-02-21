using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Shared.Models;

namespace AdventOfCode.Day20.Models
{
    public class Cheat
    {
        public Position OriginalRunStartPosition { get; set; }
        public Position Start { get; set; }
        public Position End { get; set; }
        public int TimeSaved { get; set; }

        public Cheat(Position originalRunStartPosition, Position start, Position end, int timeSaved)
        {
            OriginalRunStartPosition = originalRunStartPosition;
            Start = start;
            End = end;
            TimeSaved = timeSaved;
        }
    }
}
