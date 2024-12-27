using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day16.Models;

public class Maze
{
    public int NRows { get; set; }
    public int NColumns { get; set; }
    public Field[,] Fields { get; set; }
    public Position Start { get; set; } = new Position(-1,-1);
    public Position End { get; set; } = new Position(-1, -1);

    public Maze(int nRows, int nColumns)
    {
        NRows = nRows;
        NColumns = nColumns;
        Fields = new Field[nRows, nColumns];
    }
}
