using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day9.Models;

public class Block
{
    public int? Id { get; set; }
    public long Position { get; set; }

    public Block(int? id, long position)
    {
        Id = id;
        Position = position;
    }
}
