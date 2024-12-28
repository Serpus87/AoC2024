using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17.Instructions
{
    internal static class Adv
    {
        public static int Opcode { get; set; } = 0;

        public static int Perform(int numerator, int denominator) 
        { 
            return numerator / denominator;
        }
    }
}
