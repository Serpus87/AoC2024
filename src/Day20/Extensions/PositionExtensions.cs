using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day20.Models;

namespace AdventOfCode.Day20.Extensions
{
    public static class PositionExtensions
    {
        public static bool IsEqual(this Position position, Position other)
        {
            return position.Row == other.Row && position.Column == other.Column;
        }
    }
}
