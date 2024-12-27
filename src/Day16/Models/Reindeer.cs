using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day16.Enums;
using AdventOfCode.Day16.Extensions;

namespace AdventOfCode.Day16.Models;

public class Reindeer
{
    public Position Position { get; set; }
    public int Score { get; set; } = 0;
    public DirectionEnum Direction { get; set; } = DirectionEnum.East;
    public bool IsDoneWalking = false;
    public List<Position> PreviousPositions { get; set; } = new List<Position>();
    public int DirectionChangeCounter { get; set; } = 0;
    public int MoveCounter { get; set; } = 0;

    public Reindeer(Position position)
    {
        Position = position;
    }

    public Reindeer(Reindeer reindeerToCopy)
    {
        Position = new Position(reindeerToCopy.Position.Row, reindeerToCopy.Position.Column);
        Score = reindeerToCopy.Score;
        Direction = reindeerToCopy.Direction;
        PreviousPositions = reindeerToCopy.PreviousPositions.Copy();
        DirectionChangeCounter = reindeerToCopy.DirectionChangeCounter;
        MoveCounter = reindeerToCopy.MoveCounter;
    }

    public void MakeMove(Move move)
    {
        //todo just update score instead of counters
        if (move.Direction != Direction) {
            Direction = move.Direction;
            DirectionChangeCounter++;
            Score += 1000;
        }

        MoveCounter++;
        Score++;
        Position = new Position(Position.Row + move.Position.Row, Position.Column + move.Position.Column);
    }

    public int GetScore()
    {
        return 1000 * DirectionChangeCounter + MoveCounter;
    }
}
