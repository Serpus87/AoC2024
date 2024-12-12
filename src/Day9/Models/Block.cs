using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day9.Models;

public class Block
{
    public int? Id { get; set; }
    public int Position { get; set; }

    public Block(int? id, int position)
    {
        Id = id;
        Position = position;
    }
}
