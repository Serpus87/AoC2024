using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day12.Models;

public class Region
{
    public int Id { get; set; }
    public List<Plot> Plots { get; set; }
    public int Area { get; set; }
    public int Perimeter { get; set; }
    public int Price { get; set; }

    public Region(int id, List<Plot> plots)
    {
        Id = id;
        Plots = plots;
        Area = plots.Count;
        Perimeter = plots.Sum(x=>x.NumberOfSidesAdjacentToOtherRegion);
        Price = Area * Perimeter;
    }
}
