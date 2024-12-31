using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day18.Models;

namespace AdventOfCode.Day18.Extensions
{
    public static class PositionExtensions
    {
        public static List<Position> Copy(this List<Position> positions)
        {
            var copy = new List<Position>();

            foreach (var position in positions)
            {
                copy.Add(new Position(position.Row, position.Column));
            }

            return copy;
        }
    }
}
