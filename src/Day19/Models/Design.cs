using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day19.Models
{
    public class Design
    {
        public string Colors { get; set; }
        public bool CanBeMade { get; set; } = false;
        public List<DesignPattern> DesignPatterns { get; set; } = new List<DesignPattern>();
        public List<DesignAttempt> DesignAttempts { get; set; } = new List<DesignAttempt>();

        public Design(string colors)
        {
            Colors = colors;
        }
    }
}
