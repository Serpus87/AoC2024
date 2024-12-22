using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15.Models;

public class Warehouse
{
    public Map Map { get; set; }
    public Robot Robot { get; set; }
    public List<Box> Boxes { get; set; }

    public Warehouse(Map map, Robot robot, List<Box> boxes)
    {
        Map = map;
        Robot = robot;
        Boxes = boxes;
    }
}
