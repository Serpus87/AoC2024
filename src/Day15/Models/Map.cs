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

    public void Print(Move? move = null, int? moveCounter = null, int? totalNumberOfMoves = null)
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

        var stringToPrint = string.Empty;

        if (move != null)
        {
            if (move.Horizontal == -1) { stringToPrint += "Move: < ; "; }
            if (move.Horizontal == 1) { stringToPrint += "Move: > ; "; }
            if (move.Vertical == -1) { stringToPrint += "Move: ^ ; "; }
            if (move.Vertical == 1) { stringToPrint += "Move: v ; "; }
        }

        if (moveCounter != null)
        {
            stringToPrint += $"Move number: {moveCounter} ; ";
        }

        if (totalNumberOfMoves != null)
        {
            stringToPrint += $"Total nummber of moves: {totalNumberOfMoves}.";
        }

        if (stringToPrint.Length > 0)
        {
            Console.WriteLine(stringToPrint);
        }
    }

    public void Update(Robot robot, List<WideBox> wideBoxes)
    {
        // empty robot and box fields
        var charsToRemove = new List<char> { '@', '[', ']' };
        for (int row = 0; row < NumberOfRows; row++)
        {
            for (int column = 0; column < NumberOfColumns; column++)
            {
                if (charsToRemove.Contains(Fields[row,column].Fill))
                {
                    Fields[row, column].Fill = '.';
                }
            }
        }

        // refill robot field
        Fields[robot.Position.Row, robot.Position.Column].Fill = '@';

        // refill box fields
        foreach (var wideBox in wideBoxes)
        {
            Fields[wideBox.LeftBox.Position.Row, wideBox.LeftBox.Position.Column].Fill = '[';
            Fields[wideBox.RightBox.Position.Row, wideBox.RightBox.Position.Column].Fill = ']';
        }
    }
}
