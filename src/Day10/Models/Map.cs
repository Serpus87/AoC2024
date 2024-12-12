using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Models;

namespace AdventOfCode.Day10.Models;

public class Map
{
    public int NRows { get; set; }
    public int NColumns { get; set; }
    public Field[,] Fields { get; set; }
    public List<TrailHead> TrailHeads { get; set; } = new List<TrailHead>();
    public List<Position> TrailTails { get; set; } = new List<Position>();

    public Map(int nRows, int nColumns)
    {
        NRows = nRows;
        NColumns = nColumns;
        Fields = new Field[nRows, nColumns];
    }
}
