﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day14.Models;

public class Quadrant
{
    public List<Field> Fields { get; set; }

    public Quadrant(List<Field> fields)
    {
        Fields = fields;
    }

    public int GetNumberOfRobots()
    {
        return Fields.Sum(x=>x.NumberOfRobots);
    }
}
