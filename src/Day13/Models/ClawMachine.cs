using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day13.Models;

public class ClawMachine
{
    public Button AButton { get; set; }
    public Button BButton { get; set; }
    public Prize Prize { get; set; }

    public ClawMachine(Button aButton, Button bButton, Prize prize)
    {
        AButton = aButton; BButton = bButton; Prize = prize;
    }
}
