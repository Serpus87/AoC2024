using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day6.Models;

public class Map
{
    public int NRows { get; set; }
    public int NColumns { get; set; }
    public Field[,] Fields { get; set; }

    public Map(int nRows, int nColumns)
    {
        NRows = nRows;
        NColumns = nColumns;
        Fields = new Field[nRows, nColumns];
    }
}
