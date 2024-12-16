using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day10.Extensions;

namespace AdventOfCode.Day10.Models;

public class TrailHead
{
    public Position Position { get; init; }
    public List<Trail> Trails { get; set; } = new List<Trail>();
    public int Score { get; set; } = 0;
    public List<Position> TrailTails { get; set; } = new List<Position>();
    public List<Position> TrailEnds { get; set; } = new List<Position>();
    public int Rating { get; set; } = 0;

    public TrailHead(Position position)
    {
        Position = position;
    }

    public void AddTrailTailIfNew(Position trailTail)
    {
        if (!TrailTails.Any(x => x.Row == trailTail.Row && x.Column == trailTail.Column))
        {
            TrailTails.Add(trailTail);
            Score++;
            Rating++;
        }
    }

    public void AddTrailEndIfNew(Position trailEnd)
    {
        if (!TrailEnds.Any(x => x.Row == trailEnd.Row && x.Column == trailEnd.Column))
        {
            TrailEnds.Add(trailEnd);
        }
    }

    public void AddTrailIfNew(Trail trail)
    {
        if (!Trails.HasTrail(trail))
        {
            Trails.Add(trail);
            Rating++;
        }
    }

    public bool HasTrailEnd(Position trailEnd)
    {
        return TrailEnds.Any(x => x.Row == trailEnd.Row && x.Column == trailEnd.Column);
    }
}
