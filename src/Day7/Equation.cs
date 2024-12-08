using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day7;

public class Equation
{
    public long TestValue { get; set; }
    public List<long> Numbers { get; set; }

    public Equation(long testValue, List<long> numbers)
    {
        TestValue = testValue;
        Numbers = numbers;
    }
}
