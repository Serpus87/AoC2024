using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3;

public static class InputReader
{
    public static string Read(string fileName)
    {
        return File.ReadAllText($"Day3\\{fileName}");
    }
}
