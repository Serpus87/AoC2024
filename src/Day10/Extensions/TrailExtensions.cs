using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day10.Models;
using AdventOfCode.Day10.Extensions;

namespace AdventOfCode.Day10.Extensions;

public static class TrailExtensions
{
    public static bool HasTrail(this List<Trail> trails, Trail trailToCompare)
    {
        return trails.Any(trail => trail.Positions.AreEqual(trailToCompare.Positions));
    }
}
