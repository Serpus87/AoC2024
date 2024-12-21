using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day13.Models;

public class Location
{
    public long XCoordinate { get; set; }
    public long YCoordinate { get; set; }

    public Location(long xCoordinate, long yCoordinate)
    {
        XCoordinate = xCoordinate;
        YCoordinate = yCoordinate;
    }

    public bool IsEqual(Location other)
    {
        return XCoordinate == other.XCoordinate && YCoordinate == other.YCoordinate;
    }

    public bool IsGreaterThan(Location prizeLocation)
    {
        return XCoordinate > prizeLocation.XCoordinate || YCoordinate > prizeLocation.YCoordinate;
    }

    public bool HasSmallerThanAndSmallerOrEqualTo(Location prizeLocation)
    {
        return (XCoordinate < prizeLocation.XCoordinate && YCoordinate <= prizeLocation.YCoordinate) ||
            (XCoordinate <= prizeLocation.XCoordinate && YCoordinate < prizeLocation.YCoordinate);
    }
}
