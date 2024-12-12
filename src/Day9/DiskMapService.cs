using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        int diskIndex = 0;

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

            if (firstFreeSpaceBlock.Position >= lastFileBlock.Position)
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

    public static long CalculateCheckSum(Disk disk)
    {
        long checkSum = 0;

        foreach (var block in disk.Blocks.Where(x => x.Id != null))
        {
            checkSum += (long)(block.Id! * block.Position);
        }

        return checkSum;
    }

    public static Disk UpdateWholeFilesOnDisk(Disk disk, List<PuzzleFile> files)
    {
        var fileIds = files.Select(x => x.Id);

        for (int fileId = fileIds.Max(); fileId >= 0; fileId--)
        {
            Console.WriteLine(fileId);

            var fileSize = files.Single(x => x.Id == fileId).NumberOfBlocks;

            var blocksOfFile = disk.Blocks.Where(x => x.Id == fileId).ToList();
            var firstPositionOfFile = blocksOfFile.Min(x => x.Position);
            var firstPositionOfFreeSpace = disk.Blocks.First(x => x.Id == null).Position;

            // find freeSpace with enough size for fileSize
            var blocksOfFreeSpace = FindBlocksOfFreeSpaceLargeEnough(disk, firstPositionOfFreeSpace, firstPositionOfFile, fileSize);

            if (blocksOfFreeSpace.Count == 0)
            {
                continue;
            }

            // move file
            for (int i = 0; i < blocksOfFile.Count; i++)
            {
                var blockOfFile = blocksOfFile[i];
                var blockOfFreeSpace = blocksOfFreeSpace[i];

                blockOfFreeSpace.Id = blockOfFile.Id;
                blockOfFile.Id = null;
            }
        }

        disk.Print();
        return disk;
    }

    private static List<Block> GetFileBlocks(PuzzleFile file, int diskIndex)
    {
        var blocks = new List<Block>();
        for (int i = 0; i < file.NumberOfBlocks; i++)
        {
            blocks.Add(new Block(file.Id, diskIndex));
            diskIndex++;
        }

        return blocks;
    }

    private static List<Block> GetFreeSpaceBlocks(FreeSpace freeSpace, int diskIndex)
    {
        var blocks = new List<Block>();
        for (int i = 0; i < freeSpace.NumberOfBlocks; i++)
        {
            blocks.Add(new Block(null, diskIndex));
            diskIndex++;
        }

        return blocks;
    }

    private static List<Block> FindBlocksOfFreeSpaceLargeEnough(Disk disk, int firstPositionOfFreeSpace, int firstPositionOfFile, int fileSize)
    {
        var blocksOfFreeSpaceLargeEnough = new List<Block>();
        var possibleLastPosition = firstPositionOfFreeSpace + fileSize - 1;
        var hasBlocksOfFreeSpaceLargeEnough = false;

        while (!hasBlocksOfFreeSpaceLargeEnough)
        {
            if (possibleLastPosition >= firstPositionOfFile)
            {
                return new List<Block>();
            }

            blocksOfFreeSpaceLargeEnough = disk.Blocks.Where(x => x.Position >= firstPositionOfFreeSpace && x.Position <= possibleLastPosition).ToList();

            hasBlocksOfFreeSpaceLargeEnough = blocksOfFreeSpaceLargeEnough.All(x => x.Id == null);

            if (!hasBlocksOfFreeSpaceLargeEnough)
            {
                firstPositionOfFreeSpace = (int)disk.Blocks.First(x => x.Position >= possibleLastPosition && x.Id == null).Position;
                possibleLastPosition = firstPositionOfFreeSpace + fileSize - 1;
            }
        }

        return blocksOfFreeSpaceLargeEnough;
    }

    // this works but is too slow
    public static Disk UpdateWholeFilesOnDiskV1(Disk disk, List<PuzzleFile> files)
    {
        var fileIds = files.Select(x => x.Id);

        for (int fileId = fileIds.Max(); fileId >= 0; fileId--)
        {
            Console.WriteLine(fileId);

            var fileSize = files.Single(x => x.Id == fileId).NumberOfBlocks;

            var blocksOfFile = disk.Blocks.Where(x => x.Id == fileId).ToList();
            var firstPositionOfFile = blocksOfFile.Min(x => x.Position);
            var firstPositionOfFreeSpace = disk.Blocks.First(x => x.Id == null).Position;

            // find freeSpace with enough size for fileSize
            var blocksOfFreeSpace = FindBlocksOfFreeSpaceLargeEnough(disk, firstPositionOfFreeSpace, firstPositionOfFile, fileSize);

            if (blocksOfFreeSpace.Count == 0)
            {
                continue;
            }

            // move file
            for (int i = 0; i < blocksOfFile.Count; i++)
            {
                var blockOfFile = blocksOfFile[i];
                var blockOfFreeSpace = blocksOfFreeSpace[i];

                blockOfFreeSpace.Id = blockOfFile.Id;
                blockOfFile.Id = null;
            }
        }

        disk.Print();
        return disk;
    }

    // This also works, but is also too slow! 
    private static List<Block> FindBlocksOfFreeSpaceLargeEnoughV1(Disk disk, int firstPositionOfFreeSpace, int firstPositionOfFile, int fileSize)
    {
        var blocksOfFreeSpaceLargeEnough = new List<Block>();
        var possibleLastPosition = firstPositionOfFreeSpace + fileSize;
        var hasBlocksOfFreeSpaceLargeEnough = true;

        for (; firstPositionOfFreeSpace < possibleLastPosition; firstPositionOfFreeSpace++)
        {
            hasBlocksOfFreeSpaceLargeEnough = disk.Blocks[firstPositionOfFreeSpace].Id == null;

            if (!hasBlocksOfFreeSpaceLargeEnough)
            {
                firstPositionOfFreeSpace = (int)disk.Blocks.First(x => x.Position > firstPositionOfFreeSpace && x.Id == null).Position - 1;
                possibleLastPosition = firstPositionOfFreeSpace + fileSize + 1;
                blocksOfFreeSpaceLargeEnough = new List<Block>();

                if (possibleLastPosition >= firstPositionOfFile)
                {
                    return blocksOfFreeSpaceLargeEnough;
                }

                continue;
            }

            blocksOfFreeSpaceLargeEnough.Add(disk.Blocks[firstPositionOfFreeSpace]);
        }

        return blocksOfFreeSpaceLargeEnough;
    }

    // this works but is too slow
    private static List<Block> FindBlocksOfFreeSpaceLargeEnoughRecursive(Disk disk, int firstPositionOfFreeSpace, int firstPositionOfFile, int fileSize)
    {
        var blocksOfFreeSpaceLargeEnough = new List<Block>();
        var possibleLastPosition = firstPositionOfFreeSpace + fileSize;
        var hasBlocksOfFreeSpaceLargeEnough = true;

        if (possibleLastPosition >= firstPositionOfFile)
        {
            return blocksOfFreeSpaceLargeEnough;
        }

        for (; firstPositionOfFreeSpace < possibleLastPosition; firstPositionOfFreeSpace++)
        {
            hasBlocksOfFreeSpaceLargeEnough = disk.Blocks[firstPositionOfFreeSpace].Id == null;

            if (!hasBlocksOfFreeSpaceLargeEnough)
            {
                var newFirstPositionOfFreeSpace = (int)disk.Blocks.First(x => x.Position > firstPositionOfFreeSpace && x.Id == null).Position;
                return FindBlocksOfFreeSpaceLargeEnoughRecursive(disk, newFirstPositionOfFreeSpace, firstPositionOfFile, fileSize);
            }

            blocksOfFreeSpaceLargeEnough.Add(disk.Blocks[firstPositionOfFreeSpace]);
        }

        return blocksOfFreeSpaceLargeEnough;
    }
}
