using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day13.Models;

public class Prize
{
    public Location Location { get; set; }

    public Prize(Location location)
    {
        Location = location;
    }
}
