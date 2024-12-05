using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4;

public class Coordinate
{
    public int LineIndex { get; set; }
    public int CharIndex { get; set; }
    public char Mark { get; set; }

    public Coordinate(int lineIndex, int charIndex, char mark)
    {
        LineIndex = lineIndex;
        CharIndex = charIndex;
        Mark = mark;
    }
}
