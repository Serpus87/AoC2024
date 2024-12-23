using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15.Models;

public class WideBox
{
    public Box LeftBox { get; init; }
    public Box RightBox { get; init; }
    public List<Box> Boxes { get; init; }

    public WideBox(Box leftBox, Box rightBox)
    {
        LeftBox = leftBox;
        RightBox = rightBox;
        Boxes = new List<Box> { leftBox, rightBox};
    }
}
