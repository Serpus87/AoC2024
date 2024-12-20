using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day13.Models;

namespace AdventOfCode.Day13;

public static class ArcadeService
{
    public static List<ClawMachine> SetupMachines(string[] input)
    {
        var machines = new List<ClawMachine>();

        int xMoveAButton = 0;
        int yMoveAButton = 0;
        int xMoveBButton = 0;
        int yMoveBButton = 0;
        int xCoordinatePrize = 0;
        int yCoordinatePrize = 0;

        foreach (var line in input)
        {
            if (line.Length == 0)
            {
                continue;
            }

            var identifier = line.Substring(0, line.IndexOf(':'));

            var xValue = int.Parse(line.Substring(line.IndexOf('X') + 2, line.IndexOf(',') - (line.IndexOf('X') + 2)));
            var yValue = int.Parse(line.Substring(line.IndexOf('Y') + 2));

            switch (identifier)
            {
                case "Button A":
                    xMoveAButton = xValue;
                    yMoveAButton = yValue;
                    break;
                case "Button B":
                    xMoveBButton = xValue;
                    yMoveBButton = yValue;
                    break;
                case "Prize":
                    xCoordinatePrize = xValue;
                    yCoordinatePrize = yValue;

                    var buttonA = new Button(xMoveAButton, yMoveAButton, 3);
                    var buttonB = new Button(xMoveBButton, yMoveBButton, 1);
                    var prize = new Prize(new Location(xCoordinatePrize, yCoordinatePrize));
                    machines.Add(new ClawMachine(buttonA, buttonB, prize));
                    break;
                default:
                    break;
            }
        }

        return machines;
    }

    public static int PlayMachines(List<ClawMachine> machines)
    {
        var totalCost = 0;

        var machineCounter = 0;
        foreach (var machine in machines)
        {
            machineCounter++;
            Console.WriteLine($"Playing machine number {machineCounter} out of {machines.Count} total number of machines");
            totalCost += TryWinPrize(machine);
        }

        return totalCost;
    }

    private static int TryWinPrize(ClawMachine machine)
    {
        var costStartWithButtonA = TryWinPrizeFromStartingButton(machine.AButton, machine.BButton, machine.Prize.Location);

        // if cost is 0 price cannot be won
        if (costStartWithButtonA == 0)
        {
            return 0;
        }

        // if price can be won, lower cost may be found
        var costStartWithButtonB = TryWinPrizeFromStartingButton(machine.BButton, machine.AButton, machine.Prize.Location);

        return Math.Min(costStartWithButtonA, costStartWithButtonB);
    }

    private static int TryWinPrizeFromStartingButton(Button startingButton, Button otherButton, Location prizeLocation)
    {
        // get starting button presses
        var startingButtonPresses = GetStartingButtonPresses(startingButton, prizeLocation);

        // check for possible Prize
        var startingButtonFinalLocation = Calculate(startingButtonPresses);

        if (startingButtonFinalLocation.IsEqual(prizeLocation))
        {
            return startingButtonPresses.Sum(x => x.Token);
        }
        else
        {
            startingButtonPresses = GetFinalButtonPresses(startingButtonPresses, otherButton, prizeLocation);
        }

        if (startingButtonPresses.Count == 0)
        {
            return 0;
        }

        return startingButtonPresses.Sum(x => x.Token);
    }

    private static List<Button> GetFinalButtonPresses(List<Button> startingButtonPresses, Button otherButton, Location prizeLocation)
    {
        var finalButtonPresses = startingButtonPresses;
        var finalLocation = Calculate(finalButtonPresses);
        var prizeIsFound = finalLocation.IsEqual(prizeLocation);
        var buttonPressesAreMoot = finalButtonPresses.All(x => x.XMove == otherButton.XMove && x.YMove == otherButton.YMove);

        while (!(prizeIsFound || buttonPressesAreMoot))
        {
            while (finalLocation.IsGreaterThan(prizeLocation))
            {
                finalButtonPresses.RemoveAt(0);

                finalLocation = Calculate(finalButtonPresses);
            }
            while (finalLocation.HasSmallerThanAndSmallerOrEqualTo(prizeLocation))
            {
                finalButtonPresses.Add(otherButton);

                finalLocation = Calculate(finalButtonPresses);
            }

            prizeIsFound = finalLocation.IsEqual(prizeLocation);
            buttonPressesAreMoot = finalButtonPresses.All(x => x.XMove == otherButton.XMove && x.YMove == otherButton.YMove);
        }

        if (prizeIsFound)
        {
            return finalButtonPresses;
        }

        return new List<Button>();
    }

    private static List<Button> GetStartingButtonPresses(Button button, Location prizeLocation)
    {
        var startingButtonPresses = new List<Button>();

        var numberOfButtonPressesToGoOverPriceX = (long)Math.Ceiling((decimal)prizeLocation.XCoordinate / button.XMove);
        var numberOfButtonPressesToGoOverPriceY = (long)Math.Ceiling((decimal)prizeLocation.YCoordinate / button.YMove);

        var minButtonPresses = Math.Min(numberOfButtonPressesToGoOverPriceX, numberOfButtonPressesToGoOverPriceY);

        for (var i = 0; i <= minButtonPresses; i++)
        {
            startingButtonPresses.Add(button);
        }

        return startingButtonPresses;
    }

    private static Location Calculate(List<Button> buttonPresses)
    {
        var xCoordinate = buttonPresses.Sum(x => x.XMove);
        var yCoordinate = buttonPresses.Sum(x => x.YMove);

        return new Location(xCoordinate, yCoordinate);
    }

    public static void RecalibratePrizeLocations(List<ClawMachine> machines)
    {
        foreach (var machine in machines)
        {
            machine.Prize.Location.XCoordinate += 10000000000000;
            machine.Prize.Location.YCoordinate += 10000000000000;
        }
    }

    public static ulong SolveWithoutPlayingMachines(List<ClawMachine> machines)
    {
        ulong totalCost = 0;

        var machineCounter = 0;
        foreach (var machine in machines)
        {
            machineCounter++;
            Console.WriteLine($"Playing machine number {machineCounter} out of {machines.Count} total number of machines");
            totalCost += TryWinPrizeWithMath(machine);
        }

        return totalCost;
    }

    private static ulong TryWinPrizeWithMath(ClawMachine machine)
    {
        // --- pseudoCode AKA math ---
        /*  
         * machine.Prize.Location.XCoordinate = numberOfAButtons * machine.AButton.XMove + numberOfBButtons * machine.BButton.XMove;
         * machine.Prize.Location.YCoordinate = numberOfAButtons * machine.AButton.YMove + numberOfBButtons * machine.BButton.YMove;
         *
         * numberOfAButtons = (machine.Prize.Location.XCoordinate - numberOfBButtons * machine.BButton.XMove) / machine.AButton.XMove;
         * machine.Prize.Location.YCoordinate = ((machine.Prize.Location.XCoordinate - numberOfBButtons * machine.BButton.XMove) / machine.AButton.XMove) * machine.AButton.YMove + numberOfBButtons * machine.BButton.YMove;
         * machine.Prize.Location.YCoordinate = (machine.Prize.Location.XCoordinate * machine.AButton.YMove - numberOfBButtons * machine.BButton.XMove * machine.AButton.YMove) / machine.AButton.XMove  + numberOfBButtons * machine.BButton.YMove;
         * machine.Prize.Location.YCoordinate * machine.AButton.XMove = machine.Prize.Location.XCoordinate * machine.AButton.YMove - numberOfBButtons * machine.BButton.XMove * machine.AButton.YMove + numberOfBButtons * machine.BButton.YMove * machine.AButton.XMove;
         * machine.Prize.Location.YCoordinate * machine.AButton.XMove - machine.Prize.Location.XCoordinate * machine.AButton.YMove =  - numberOfBButtons * machine.BButton.XMove * machine.AButton.YMove + numberOfBButtons * machine.BButton.YMove * machine.AButton.XMove;
         * machine.Prize.Location.YCoordinate * machine.AButton.XMove - machine.Prize.Location.XCoordinate * machine.AButton.YMove = numberOfBButtons * machine.BButton.YMove * machine.AButton.XMove - numberOfBButtons * machine.BButton.XMove * machine.AButton.YMove;
         * machine.Prize.Location.YCoordinate * machine.AButton.XMove - machine.Prize.Location.XCoordinate * machine.AButton.YMove = numberOfBButtons * (machine.BButton.YMove * machine.AButton.XMove - machine.BButton.XMove * machine.AButton.YMove);
         * 
         * numberOfBButtons = (machine.Prize.Location.YCoordinate * machine.AButton.XMove - machine.Prize.Location.XCoordinate * machine.AButton.YMove) / (machine.BButton.YMove * machine.AButton.XMove - machine.BButton.XMove * machine.AButton.YMove);
         * numberOfAButtons = (machine.Prize.Location.XCoordinate - numberOfBButtons * machine.BButton.XMove) / machine.AButton.XMove;
         *
         * 
         */

        // 4 possible solutions

        // first solve for B on XPrizeLocation
        var numberOfBButtons1 = (decimal)(machine.Prize.Location.YCoordinate * machine.AButton.XMove - machine.Prize.Location.XCoordinate * machine.AButton.YMove) / (machine.BButton.YMove * machine.AButton.XMove - machine.BButton.XMove * machine.AButton.YMove);
        var numberOfAButtons1 = (machine.Prize.Location.XCoordinate - numberOfBButtons1 * machine.BButton.XMove) / machine.AButton.XMove;

        if (Math.Ceiling(numberOfAButtons1) != Math.Floor(numberOfAButtons1))
        {
            return 0;
        }

        var cost1 = (ulong)machine.AButton.Token * (ulong)numberOfAButtons1 + (ulong)machine.BButton.Token * (ulong)numberOfBButtons1;

        // second solve for B on YPrizeLocation
        var numberOfBButtons2 = (decimal)(machine.Prize.Location.XCoordinate * machine.AButton.YMove - machine.Prize.Location.YCoordinate * machine.AButton.XMove) / (machine.BButton.XMove * machine.AButton.YMove - machine.BButton.YMove * machine.AButton.XMove);
        var numberOfAButtons2 = (machine.Prize.Location.YCoordinate - numberOfBButtons2 * machine.BButton.YMove) / machine.AButton.YMove;

        var cost2 = (ulong)machine.AButton.Token * (ulong)numberOfAButtons2 + (ulong)machine.BButton.Token * (ulong)numberOfBButtons2;

        // third solve for A on XPrizeLocation
        var numberOfAButtons3 = (decimal)(machine.Prize.Location.YCoordinate * machine.BButton.XMove - machine.Prize.Location.XCoordinate * machine.BButton.YMove) / (machine.AButton.YMove * machine.BButton.XMove - machine.AButton.XMove * machine.BButton.YMove);
        var numberOfBButtons3 = (machine.Prize.Location.XCoordinate - numberOfAButtons3 * machine.AButton.XMove) / machine.BButton.XMove;

        var cost3 = (ulong)machine.AButton.Token * (ulong)numberOfAButtons3 + (ulong)machine.BButton.Token * (ulong)numberOfBButtons3;

        // fourth solve for A on YPrizeLocation
        var numberOfAButtons4 = (decimal)(machine.Prize.Location.XCoordinate * machine.BButton.YMove - machine.Prize.Location.YCoordinate * machine.BButton.XMove) / (machine.AButton.XMove * machine.BButton.YMove - machine.AButton.YMove * machine.BButton.XMove);
        var numberOfBButtons4 = (machine.Prize.Location.YCoordinate - numberOfAButtons4 * machine.AButton.YMove) / machine.BButton.YMove;

        var cost4 = (ulong)machine.AButton.Token * (ulong)numberOfAButtons4 + (ulong)machine.BButton.Token * (ulong)numberOfBButtons4;


        // -- temp doubleCheck the math
        var sol1 = DoubleCheck(machine, (ulong)numberOfAButtons1, (ulong)numberOfBButtons1);
        var sol2 = DoubleCheck(machine, (ulong)numberOfAButtons2, (ulong)numberOfBButtons2);
        var sol3 = DoubleCheck(machine, (ulong)numberOfAButtons3, (ulong)numberOfBButtons3);
        var sol4 = DoubleCheck(machine, (ulong)numberOfAButtons4, (ulong)numberOfBButtons4);
        // --

        // return minimumCost
        return Math.Min(Math.Min(cost1,cost2),Math.Min(cost3,cost4));
    }

    private static bool DoubleCheck(ClawMachine machine, ulong aButtonPresses, ulong bButtonPresses)
    {
        var xCoordinate = aButtonPresses * (ulong)machine.AButton.XMove + bButtonPresses * (ulong)machine.BButton.XMove;
        var yCoordinate = aButtonPresses * (ulong)machine.AButton.YMove + bButtonPresses * (ulong)machine.BButton.YMove;

        var location = new Location((long)xCoordinate, (long)yCoordinate);
        var locationIsPrizeLocation = location.IsEqual(machine.Prize.Location);

        return locationIsPrizeLocation;
    }
}
