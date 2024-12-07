using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day6.Models;

public class PrintingMap
{
    public int FirstRow { get; set; }
    public int LastRow { get; set; }
    public int FirstColumn { get; set; }
    public int LastColumn { get; set; }

    public PrintingMap(int firstRow, int lastRow, int firstColumn, int lastColumn)
    {
        FirstRow = firstRow;
        LastRow = lastRow;
        FirstColumn = firstColumn;
        LastColumn = lastColumn;
    }
}
