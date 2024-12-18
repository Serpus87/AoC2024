using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day11;

public class Stone
{
    public long Number { get; set; }
    public long Count { get; set; }

    public Stone(long number)
    {
        Number = number;
        Count = 1;
    }

    public Stone(long number, long count)
    {
        Number = number;
        Count = count;
    }
}
