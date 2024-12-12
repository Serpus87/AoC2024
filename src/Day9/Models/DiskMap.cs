using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day9.Models;

public class DiskMap
{
    public List<PuzzleFile> Files { get; set; }
    public List<FreeSpace> FreeSpaces { get; set; }

    public DiskMap(List<PuzzleFile> files, List<FreeSpace> freeSpaces)
    {
        Files = files;
        FreeSpaces = freeSpaces;
    }
}
