using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15.Models;

public class Warehouse : BaseWarehouse
{
    public List<WideBox> Boxes { get; set; }

    public Warehouse(Map map, Robot robot, List<WideBox> boxes) : base(map, robot)
    {
        Boxes = boxes;
    }
}
