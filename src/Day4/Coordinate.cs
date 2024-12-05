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
    public Mark ExptectedMark { get; set; }
    public Mark ActualMark { get; set; }

    public Coordinate(int lineIndex, int charIndex, Mark expectedMark)
    {
        LineIndex = lineIndex;
        CharIndex = charIndex;
        ExptectedMark = expectedMark;
    }

    public Coordinate(int lineIndex, int charIndex)
    {
        LineIndex = lineIndex;
        CharIndex = charIndex;
        ExptectedMark = Mark.X;
        ActualMark = Mark.X;
    }

    public bool IsActualMarkExpectedMark()
    {
        return ActualMark == ExptectedMark;
    }
}
