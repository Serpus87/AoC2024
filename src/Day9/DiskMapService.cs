using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day9.Models;

namespace AdventOfCode.Day9;

public static class DiskMapService
{
    public static DiskMap GetDiskMap(string input)
    {
        var puzzleFiles = new List<PuzzleFile>();
        var freeSpaces = new List<FreeSpace>();
        var idCounter = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var numberOfBlocks = (int)Char.GetNumericValue(input[i]);

            if (i % 2 == 0)
            {
                puzzleFiles.Add(new PuzzleFile(idCounter, numberOfBlocks));
                idCounter++;
                continue;
            }

            freeSpaces.Add(new FreeSpace('.', numberOfBlocks));
        }

        return new DiskMap(puzzleFiles, freeSpaces);
    }

    public static Disk GetDisk(DiskMap diskMap)
    {
        var disk = new Disk();
        var diskLength = diskMap.Files.Count + diskMap.FreeSpaces.Count;
        var blocks = new List<Block>();

        var fileCounter = 0;
        var freeSpaceCounter = 0;
        long diskIndex = 0;

        for (int i = 0; i < diskLength; i++)
        {
            if (i % 2 == 0)
            {
                var file = diskMap.Files[fileCounter];
                blocks = GetFileBlocks(file, diskIndex);

                fileCounter++;
            }
            else
            {
                var freeSpace = diskMap.FreeSpaces[freeSpaceCounter];
                blocks = GetFreeSpaceBlocks(freeSpace, diskIndex);
                freeSpaceCounter++;
            }

            disk.Blocks.AddRange(blocks);
            diskIndex = diskIndex + blocks.Count;
        }

        disk.Print();
        return disk;
    }

    public static Disk UpdateDisk(Disk disk)
    {
        while (true)
        {
            var firstFreeSpaceBlock = disk.Blocks.First(x => x.Id == null);
            var lastFileBlock = disk.Blocks.Last(x => x.Id != null);

            if(firstFreeSpaceBlock.Position >= lastFileBlock.Position)
            {
                break;
            };

            firstFreeSpaceBlock.Id = lastFileBlock.Id;
            lastFileBlock.Id = null;

            //disk.Print();
        }

        disk.Print();
        return disk;
    }

    internal static long CalculateCheckSum(Disk disk)
    {
        long checkSum = 0;

        foreach (var block in disk.Blocks.Where(x => x.Id != null))
        {
            checkSum += (long)(block.Id! * block.Position);
        }

        return checkSum;
    }

    private static List<Block> GetFileBlocks(PuzzleFile file, long diskIndex)
    {
        var blocks = new List<Block>();
        for (int i = 0; i < file.NumberOfBlocks; i++)
        {
            blocks.Add(new Block(file.Id, diskIndex));
            diskIndex++;
        }

        return blocks;
    }

    private static List<Block> GetFreeSpaceBlocks(FreeSpace freeSpace, long diskIndex)
    {
        var blocks = new List<Block>();
        for (int i = 0; i < freeSpace.NumberOfBlocks; i++)
        {
            blocks.Add(new Block(null, diskIndex));
            diskIndex++;
        }

        return blocks;
    }
}
