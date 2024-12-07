using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day6.Models;

public interface IFill
{
    public FillEnum FillEnum { get; set; }
    public bool IsPassable { get; set; }
    public bool IsOverWritable { get; set; }
}
