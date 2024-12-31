using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day18.Extensions;

namespace AdventOfCode.Day18.Models
{
    public class PathFinder
    {
        public Position Position { get; set; }
        public bool IsDoneWalking = false;
        public List<Position> PreviousPositions { get; set; } = new List<Position>();
        public int MoveCounter { get; set; } = 0;

        public PathFinder(Position position)
        {
            Position = position;
        }

        public PathFinder(PathFinder pathFinderToCopy)
        {
            Position = new Position(pathFinderToCopy.Position.Row, pathFinderToCopy.Position.Column);
            PreviousPositions = pathFinderToCopy.PreviousPositions.Copy();
            MoveCounter = pathFinderToCopy.MoveCounter;
        }

        public void GoToNewPosition(Position newPosition)
        {
            MoveCounter++;
            Position = new Position(newPosition.Row, newPosition.Column);
        }
    }
}
