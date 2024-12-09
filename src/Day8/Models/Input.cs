using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day8.Models;

public class Input
{
    public Map Map { get; set; }
    public  List<Antenna> Antennas { get; set; }

    public Input(Map map, List<Antenna> antennas)
    {
        Map = map;
        Antennas = antennas;
    }
}
