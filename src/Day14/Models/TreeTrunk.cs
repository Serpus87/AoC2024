using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day14.Models;

public class TreeTrunk
{
    public List<Robot> Trunk { get; set; }

    public TreeTrunk(List<Robot> trunk)
    {
        Trunk = trunk;
    }
}
