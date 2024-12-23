using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15.Models;

public class Map
{
    public int NumberOfRows { get; set; }
    public int NumberOfColumns { get; set; }
    public Field[,] Fields { get; set; }
    public List<Field> FieldsList { get; set; } = new List<Field>();

    public Map(int numberOfRows, int numberOfColumns)
    {
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
        Fields = new Field[numberOfRows, numberOfColumns];
    }

    public void Print()
    {
        Console.Clear();

        for (var row = 0; row < NumberOfRows; row++)
        {
            var rowToPrint = new List<string>();
            for (var column = 0; column < NumberOfColumns; column++)
            {
                rowToPrint.Add(Fields[row, column].Fill.ToString());
            }

            Console.WriteLine(string.Join(' ', rowToPrint));
        }
    }
}
