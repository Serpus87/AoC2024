using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15.Models;

public class BaseWarehouse
{
    public Map Map { get; set; }
    public Robot Robot { get; set; }

    public BaseWarehouse(Map map, Robot robot)
    {
        Map = map;
        Robot = robot;
    }
}
