using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15.Models;

public class Warehouse : BaseWarehouse
{
    public List<Box> Boxes { get; set; }

    public Warehouse(Map map, Robot robot, List<Box> boxes) : base(map, robot)
    {
        Boxes = boxes;
    }
}
