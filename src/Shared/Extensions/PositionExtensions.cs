using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Shared.Models;

namespace AdventOfCode.Shared.Extensions
{
    public static class PositionExtensions
    {
        public static List<Position> GetDistinct(this List<Position> positions)
        {
            var distinctPositions = new List<Position>();

            foreach (var position in positions) 
            {
                if (distinctPositions.Includes(position))
                {
                    continue;
                }

                distinctPositions.Add(position);
            }

            return distinctPositions;
        }

        public static bool Includes(this List<Position> positions, Position positionToCheck)
        {
            return positions.Any(x => x.IsEqual(positionToCheck));
        }

        public static bool IsEqual(this Position position, Position positionToCompare) 
        {
            return position.Row == positionToCompare.Row && position.Column == positionToCompare.Column;
        }
    }
}
