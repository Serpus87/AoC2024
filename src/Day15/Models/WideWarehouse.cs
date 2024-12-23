using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15.Models;

public class WideWarehouse : BaseWarehouse
{
    public List<WideBox> WideBoxes { get; set; }
    public WideWarehouse(Map map, Robot robot, List<WideBox> wideBoxes) : base(map, robot)
    {
        WideBoxes = wideBoxes;
    }
}
