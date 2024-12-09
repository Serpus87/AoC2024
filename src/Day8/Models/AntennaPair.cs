using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day8.Models;

public class AntennaPair
{
    public Antenna Antenna1 { get; set; }
    public Antenna Antenna2 { get; set; }

    public AntennaPair(Antenna antenna1, Antenna antenna2)
    {
        Antenna1 = antenna1;
        Antenna2 = antenna2;
    }
}
