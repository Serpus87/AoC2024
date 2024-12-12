using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day9.Models;

public class Disk
{
    public List<Block> Blocks { get; set; } = new List<Block>();

    public void Print()
    {
        // todo fix this
        //Console.WriteLine(string.Join(' ', Blocks.Select(x => x.Id.ToString()).ToList()));

        var stringToPrint = new List<string>();

        foreach (var block in Blocks)
        {
            if (block.Id is null)
            {
                stringToPrint.Add(".");
            }
            else
            {
                stringToPrint.Add(block.Id.ToString());
            }
        }

        Console.WriteLine(string.Join(' ', stringToPrint));
    }
}
