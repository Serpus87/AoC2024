using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day13.Models;

public class Button
{
    public int XMove { get; set; }
    public int YMove { get; set; }
    public int Token { get; set; }

    public Button(int xMove, int yMove, int token)
    {
        XMove = xMove;
        YMove = yMove;
        Token = token;
    }
}
