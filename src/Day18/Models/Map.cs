using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day18.Models
{
    public class Map
    {
        public int NRows { get; init; }
        public int NColumns { get; init; }
        public Field[,] Fields { get; set; }
        public List<Field> FieldsList { get; set; } = new List<Field>();
        public Position Start { get; init; } = new Position(0, 0);
        public Position End { get; set; }

        public Map(int nRows, int nColumns)
        {
            NRows = nRows;
            NColumns = nColumns;
            Fields = new Field[nRows, nColumns];
            End = new Position(nRows - 1, nColumns - 1);
        }

        public void ResetFields()
        {
            foreach (Field field in FieldsList.Where(x => !x.IsCorrupted))
            {
                field.LowestNumberOfMoves = null;
            }
        }
    }
}
