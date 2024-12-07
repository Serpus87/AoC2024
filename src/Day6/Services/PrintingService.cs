using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day6.Models;

namespace AdventOfCode.Day6.Services;

public static class PrintingService
{
    // todo make this work
    public static void Print(Map map, Position guardPosition)
    {
        //Console.Clear();

        var numberOfRowsAndColumnsToPrint = 9;
        PrintingMap mapToPrint = GetPrintingMap(map, guardPosition, numberOfRowsAndColumnsToPrint);

        for (var row = mapToPrint.FirstRow; row <= mapToPrint.LastRow; row++)
        {
            var rowToPrint = new List<char>();
            for (var column = mapToPrint.FirstColumn; column <= mapToPrint.LastColumn; column++)
            {
                rowToPrint.Add(map.Fields[row, column].Fill);
            }

            Console.WriteLine(string.Join(' ', rowToPrint));
        }
    }

    private static PrintingMap GetPrintingMap(Map map, Position guardPosition, int numberOfRowsAndColumnsToPrint)
    {
        var regularArmsLength = (numberOfRowsAndColumnsToPrint - 1) / 2;
        var firstRow = guardPosition.Row - regularArmsLength;
        var lastRow = guardPosition.Row + regularArmsLength;
        var firstColumn = guardPosition.Column - regularArmsLength;
        var lastColumn = guardPosition.Column + regularArmsLength;

        if (firstRow < 0)
        {
            firstRow = 0;
            lastRow = numberOfRowsAndColumnsToPrint;
        }

        if (lastRow >= map.NRows)
        {
            lastRow = map.NRows - 1;
            firstRow = lastRow - numberOfRowsAndColumnsToPrint;
        }

        if (firstColumn < 0)
        {
            firstColumn = 0;
            lastColumn = numberOfRowsAndColumnsToPrint;
        }

        if (lastColumn >= map.NColumns)
        {
            lastColumn = map.NColumns - 1;
            firstColumn = lastRow - numberOfRowsAndColumnsToPrint;
        }

        return new PrintingMap(firstRow,lastRow, firstColumn, lastColumn);
    }
}
