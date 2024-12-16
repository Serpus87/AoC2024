using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day11;

public static class BlinkingService
{
    public static List<long> Blink(List<long> stones, int numberOfBlinks)
    {
        var stonesToApplyRulesTo = stones;
        for (int i = 0; i < numberOfBlinks; i++)
        {
            //Console.WriteLine($"Blink {i + 1} out of {numberOfBlinks} total number of blinks");
            var newStones = ApplyBlinkingRules(stonesToApplyRulesTo);

            stonesToApplyRulesTo = newStones;
        }

        return stonesToApplyRulesTo;
    }

    public static void BlinkWithHelpFromFile(int numberOfBlinks)
    {
        var oldFileName = $"File0.txt";
        var newFileName = $"File1.txt";

        for (int i = 0; i < numberOfBlinks; i++)
        {
            Console.WriteLine($"Blink {i + 1} out of {numberOfBlinks} total number of blinks");

            if (i % 2 == 0)
            {
                oldFileName = $"File0.txt";
                newFileName = $"File1.txt";
            }
            else
            {
                oldFileName = $"File1.txt";
                newFileName = $"File0.txt";
            }

            string oldPath = $"Day11\\{oldFileName}";
            string newPath = $"Day11\\{newFileName}";

            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(newPath))
            {
            }

            // read from old file, process stone, write to new file
            using (StreamReader sr = File.OpenText(oldPath))
            {
                string s = "";
                var writtenStones = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    var stonesToProcess = s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
                    var processedStones = ApplyBlinkingRules(stonesToProcess);

                    // make groups of 1000 and write
                    var numberOfGroups = ((processedStones.Count - 1) / 1000) + 1;

                    for (int group = 0; group < numberOfGroups; group++)
                    {
                        var stringToPrint = String.Join(" ", processedStones.Skip(1000 * group).Take(1000));
                        File.AppendAllText(newPath, stringToPrint);
                        File.AppendAllText(newPath, Environment.NewLine);
                    }


                    //foreach (var processedStone in processedStones)
                    //{
                    //    File.AppendAllText(newPath, $"{processedStone} ");
                    //    writtenStones++;

                    //    if(writtenStones == 100)
                    //    {
                    //        File.AppendAllText(newPath, Environment.NewLine);
                    //    }
                    //}
                }
            }
        }
    }

    private static List<long> ApplyBlinkingRules(List<long> stonesToApplyRulesTo)
    {
        var newStones = new List<long>();
        for (int i = 0; i < stonesToApplyRulesTo.Count; i++)
        {
            newStones.AddRange(ApplyBlinkingRule(stonesToApplyRulesTo[i]));
        }

        return newStones;
    }

    private static List<long> ApplyBlinkingRule(long stone)
    {
        var stonesToAdd = new List<long>();
        var stoneString = stone.ToString();

        if (stone == 0)
        {
            stonesToAdd.Add(1);
            return stonesToAdd;
        }
        if (stoneString.Length % 2 == 0)
        {
            var halfWay = stoneString.Length / 2;
            var firstStone = int.Parse(stoneString.Substring(0, halfWay));
            var secondStone = int.Parse(stoneString.Substring(halfWay, halfWay));

            stonesToAdd.Add(firstStone);
            stonesToAdd.Add(secondStone);
            return stonesToAdd;
        }

        stonesToAdd.Add(stone * 2024);

        return stonesToAdd;
    }

    public static void WriteStonesToFile(List<long> stones, string fileName)
    {
        string path = $"Day11\\{fileName}";

        // Create a file to write to.
        string createText = string.Empty;
        File.WriteAllText(path, createText);

        // This text is always added, making the file longer over time
        // if it is not deleted.
        foreach (var stone in stones)
        {
            File.AppendAllText(path, $"{stone} ");
        }
    }

    public static int CountStonesFromFile(string fileName)
    {
        string path = $"Day11\\{fileName}";

        var stoneCounter = 0;

        using (StreamReader sr = File.OpenText(path))
        {
            string s = "";
            var writtenStones = 0;
            while ((s = sr.ReadLine()) != null)
            {
                var processedStones = s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
                stoneCounter += processedStones.Count;
            }
        }

        return stoneCounter;
    }
}