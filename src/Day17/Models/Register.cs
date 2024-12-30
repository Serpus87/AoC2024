using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Models;

public class Register
{
    public string Name { get; init; }
    public uint Value { get; set; }

    public Register(string name, uint value)
    {
        Name = name;
        Value = value;
    }
}
