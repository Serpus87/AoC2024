using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Enums;

namespace AdventOfCode.Day6.Models;

public class Guard
{
    public Position Position { get; set; }
    public MoveDirection MoveDirection { get; set; }
    public List<Position> Moves { get; set; }
    public List<ObstructionEncounter> ObstructionEncounters { get; set; } = new List<ObstructionEncounter>();
    public bool IsOnMap { get; set; } = true;
    public int MoveCounter { get; set; }

    public Guard(Position position)
    {
        Position = position;
        MoveDirection = MoveDirection.Up;
        Moves = new List<Position> { position };
        MoveCounter = 1;
    }

    public void TurnRight90Degrees()
    {
        switch (MoveDirection)
        {
            case MoveDirection.Up: MoveDirection = MoveDirection.Right; break;
            case MoveDirection.Right: MoveDirection = MoveDirection.Down; break;
            case MoveDirection.Down: MoveDirection = MoveDirection.Left; break;
            case MoveDirection.Left: MoveDirection = MoveDirection.Up; break;
        }
    }

    //public List<Position> GetDistinctMoves()
    //{
    //    var result = new List<Position>();

    //    foreach (var position in Moves)
    //    {
    //        if (!result.Contains(x=>x.Row == position.Row && x.Column == ))
    //        {

    //        }
    //    }
    //}
}
