using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day9.Models;

public class FreeSpace
{
    public char Id { get; set; } = '.';
    public int NumberOfBlocks { get; set; }

    public FreeSpace(char id, int numberOfBlocks)
    {
        Id = id;
        NumberOfBlocks = numberOfBlocks;
    }
}
