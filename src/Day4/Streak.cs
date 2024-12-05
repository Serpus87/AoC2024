using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4;

public class Streak
{
    public List<Coordinate> Coordinates { get; set; }

    public Streak(List<Coordinate> coordinates)
    {
        Coordinates = coordinates;
    }

    public bool StreakSpellsWordOfInterest(string word)
    {
        var streakChars = Coordinates.Select(x => x.Mark).ToArray();
        var streakWord = new string(streakChars);

        return streakWord == word;
    }
}
