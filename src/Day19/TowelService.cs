using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Models;
using AdventOfCode.Day19.Extensions;
using System.Diagnostics;

namespace AdventOfCode.Day19
{
    public static class TowelService
    {
        public static List<string[]> SplitInput(string[] input)
        {
            var splitInput = new List<string[]>();
            var patternStrings = new List<string>();
            var designStrings = new List<string>();
            var newLineIsFound = false;

            foreach (var line in input)
            {
                if (line.Length == 0)
                {
                    newLineIsFound = true;
                    continue;
                }
                if (!newLineIsFound)
                {
                    patternStrings.Add(line);
                    continue;
                }
                if (newLineIsFound)
                {
                    designStrings.Add(line);
                    continue;
                }
            }

            splitInput.Add(patternStrings.ToArray());
            splitInput.Add(designStrings.ToArray());

            return splitInput;
        }

        public static List<string> GetPatterns(string input)
        {
            var patterns = new List<string>();
            var pattern = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ',' || input[i] == ' ')
                {
                    if (pattern != string.Empty)
                    {
                        patterns.Add(pattern);
                        pattern = string.Empty;
                    }
                    continue;
                }
                pattern = pattern + input[i];
            }

            patterns.Add(pattern);

            return patterns;
        }

        public static List<Design> GetDesigns(string[] input)
        {
            var designs = new List<Design>();

            for (int i = 0; i < input.Length; i++)
            {
                designs.Add(new Design(input[i]));
            }

            return designs;
        }

        public static List<Design> FindDesignsThatCanBeMade(List<Design> designs, List<string> patterns)
        {
            var designsThatCanBeMade = new List<Design>();

            foreach (Design design in designs)
            {
                var canDesignBeMade = CanDesignBeMade(design, design.Colors, patterns, new List<DesignAttempt>(), new DesignPattern(), new DesignPattern());

                if (canDesignBeMade)
                {
                    design.CanBeMade = true;
                    design.DesignCounter++;
                    designsThatCanBeMade.Add(design);
                }
            }

            return designsThatCanBeMade;
        }

        private static bool CanDesignBeMade(Design originalDesign, string remainingDesignColors, List<string> patterns, List<DesignAttempt> designAttempts, DesignPattern originalDesignPattern, DesignPattern finalDesignPattern)
        {
            if (remainingDesignColors.Length == 0)
            {
                originalDesign.DesignPatterns.Add(finalDesignPattern);
                //originalDesign.DesignAttempts.AddRange(designAttempts);

                return true;
            }

            var matchingPatterns = FindMatchingPatterns(remainingDesignColors, patterns);
            matchingPatterns = RemovePreviouslyAttemptedPatterns(matchingPatterns, originalDesign, remainingDesignColors, designAttempts);

            if (matchingPatterns.Count == 0 && remainingDesignColors == originalDesign.Colors)
            {
                return false;
            }

            if (matchingPatterns.Count == 0)
            {
                // add attempted pattern to attempted patterns
                var designAttemptSubstring = originalDesign.Colors.Substring(0, originalDesign.Colors.Length - remainingDesignColors.Length);
                designAttempts.Add(new DesignAttempt(designAttemptSubstring));

                var originalDesignCopy = originalDesign.Colors;
                var originalDesignPatternCopy = new DesignPattern();
                originalDesignPatternCopy.Patterns.AddRange(originalDesignPattern.Patterns);

                return CanDesignBeMade(originalDesign, originalDesignCopy, patterns, designAttempts, originalDesignPattern, originalDesignPatternCopy);
            }

            foreach (string pattern in matchingPatterns)
            {
                var patternLength = pattern.Length;
                var newRemainingDesign = remainingDesignColors.Substring(patternLength);
                finalDesignPattern.Patterns.Add(pattern);

                return CanDesignBeMade(originalDesign, newRemainingDesign, patterns, designAttempts, originalDesignPattern, finalDesignPattern);
            }

            return false;
        }

        private static List<string> RemovePreviouslyAttemptedPatterns(List<string> matchingPatterns, Design originalDesign, string remainingDesignColors, List<DesignAttempt> designAttempts)
        {
            var cleanedMatchingPatterns = matchingPatterns.Copy();

            var finishedDesign = new Design(originalDesign.Colors.Substring(0, originalDesign.Colors.Length - remainingDesignColors.Length));

            foreach (var designAttempt in designAttempts)
            {
                if (designAttempt.Colors.Length + remainingDesignColors.Length < originalDesign.Colors.Length)
                {
                    continue;
                }

                foreach (var machtingPattern in matchingPatterns)
                {
                    if (finishedDesign.Colors + machtingPattern == designAttempt.Colors)
                    {
                        cleanedMatchingPatterns = cleanedMatchingPatterns.Where(x => x != machtingPattern).ToList();
                    }
                }
            }

            return cleanedMatchingPatterns;
        }

        private static List<string> FindMatchingPatterns(string designColors, List<string> patterns)
        {
            var matchingPatterns = new List<string>();

            foreach (string pattern in patterns.Where(x => x.Length <= designColors.Length))
            {
                var patternLength = pattern.Length;

                var designSubstring = designColors.Substring(0, patternLength);

                if (designSubstring == pattern)
                {
                    matchingPatterns.Add(pattern);
                }
            }

            return matchingPatterns;
        }

        public static void FindAlternativeDesignsForDesignsThatCanBeMade(List<Design> designsThatCanBeMade, List<string> patterns)
        {
            foreach (var design in designsThatCanBeMade)
            {
                AddDesignPatternEndings(design, design.DesignPatterns.First());
                AddDesignPatternStarts(design, design.DesignPatterns.First());
                var designRelevantPatterns = GetDesignRelevantPatterns(design, patterns);
                var patternsWithSameStartingLetter = GetPatternsWithSameStartingLetter(designRelevantPatterns);
                //FindAlternativeDesigns(design, designRelevantPatterns, patternsWithSameStartingLetter);
                FindAlternativeDesignsV2(design, designRelevantPatterns, patternsWithSameStartingLetter);
            }
        }

        private static void AddDesignPatternStarts(Design design, DesignPattern designPattern)
        {
            var designPatternStarts = new List<DesignPatternEnding>();
            var designPatternStart = new List<string>();

            for (int i = 0; i < (designPattern.Patterns.Count - 1); i++)
            {
                designPatternStart.Add(designPattern.Patterns[i]);

                var newDesignPatternStart = new List<string>();
                newDesignPatternStart.AddRange(designPatternStart);

                design.DesignPatternStarts.Add(new DesignPatternStart(newDesignPatternStart));
            }
        }

        private static Dictionary<string, List<string>> GetPatternsWithSameStartingLetter(List<string> designRelevantPatterns)
        {
            var patternsWithSameStartingLetter = new Dictionary<string, List<string>>();

            foreach (var pattern in designRelevantPatterns)
            {
                patternsWithSameStartingLetter.Add(pattern, designRelevantPatterns.Where(x => x[0] == pattern[0] && x != pattern).ToList());
            }

            return patternsWithSameStartingLetter;
        }

        private static void AddDesignPatternEndings(Design design, DesignPattern designPattern)
        {
            var designPatternEndings = new List<DesignPatternEnding>();
            var comboEnd = new List<string>();
            comboEnd.AddRange(designPattern.Patterns);

            for (int i = 0; i < (designPattern.Patterns.Count - 1); i++)
            {
                comboEnd.RemoveAt(0);

                var newComboEnd = new List<string>();
                newComboEnd.AddRange(comboEnd);

                design.DesignPatternEndings.AddIfNew(new DesignPatternEnding(newComboEnd));
            }
        }

        private static List<string> GetDesignRelevantPatterns(Design design, List<string> patterns)
        {
            var relevantPatterns = new List<string>();

            foreach (var pattern in patterns)
            {
                if (design.Colors.Contains(pattern))
                {
                    relevantPatterns.Add(pattern);
                }
            }

            return relevantPatterns;
        }

        private static void FindAlternativeDesignsV2(Design design, List<string> patterns, Dictionary<string, List<string>> patternsWithSameStartingLetter)
        {
            var originalDesignPattern = design.DesignPatterns.First();
            var newDesignPatterns = new List<DesignPattern> { originalDesignPattern };

            while (true)
            {
                var designPatternsToCheck = newDesignPatterns.ToList();
                newDesignPatterns = new List<DesignPattern>();

                foreach (var designPattern in designPatternsToCheck)
                {
                    // get alternativesToCheck
                    var alternativesToCheck = GetAlternativesToCheck(designPattern, design, patternsWithSameStartingLetter);

                    // check alternatives
                    foreach (var alternativeToCheck in alternativesToCheck)
                    {
                        //design.DesignPatternStarts.Add(new DesignPatternStart(alternativeToCheck.AlternativeDesignPattern.Patterns.Copy()));

                        if (design.DesignPatternEndings.Any(x => x.PatternEndDesign.Equals(alternativeToCheck.NewDesignSubstring)))
                        {
                            var alternativeDesignPatternEndings = design.DesignPatternEndings.Where(x => x.PatternEndDesign == alternativeToCheck.NewDesignSubstring).ToList();

                            foreach (var alternativeDesignPatternEnding in alternativeDesignPatternEndings)
                            {
                                var newDesignPattern = new DesignPattern();
                                newDesignPattern.Patterns.AddRange(alternativeToCheck.AlternativeDesignPattern.Patterns);
                                newDesignPattern.Patterns.AddRange(alternativeDesignPatternEnding.PatternEnd);

                                if (design.DesignPatterns.Includes(newDesignPattern))
                                {
                                    var tempInspectSomethingBad = true;
                                }

                                if (!design.DesignPatterns.Includes(newDesignPattern))
                                {
                                    newDesignPatterns.Add(newDesignPattern);
                                    design.DesignPatterns.Add(newDesignPattern);
                                } 
                            }

                            continue;
                        }

                        var newRemainingDesign = new Design(alternativeToCheck.NewDesignSubstring);

                        var canAlternativeDesignBeMade = CanDesignBeMade(newRemainingDesign, newRemainingDesign.Colors, patterns, new List<DesignAttempt>(), new DesignPattern(), new DesignPattern());

                        if (canAlternativeDesignBeMade)
                        {
                            alternativeToCheck.AlternativeDesignPattern.Patterns.AddRange(newRemainingDesign.DesignPatterns.First().Patterns);
                            if (!design.DesignPatterns.Includes(alternativeToCheck.AlternativeDesignPattern))
                            {
                                newDesignPatterns.Add(alternativeToCheck.AlternativeDesignPattern);
                                design.DesignPatterns.Add(alternativeToCheck.AlternativeDesignPattern);
                                AddDesignPatternEndings(design, alternativeToCheck.AlternativeDesignPattern);
                            }
                        }
                    }
                }

                if (designPatternsToCheck.Count == 0)
                {
                    break;
                }
            }
        }

        private static List<AlternativeDesignPatternToCheck> GetAlternativesToCheck(DesignPattern designPattern, Design design, Dictionary<string, List<string>> patternsWithSameStartingLetter)
        {
            var alternativesToCheck = new List<AlternativeDesignPatternToCheck>();
            var chronoDesignPatterns = new List<string>();
            var newDesignSubString = design.Colors;

            foreach (var pattern in designPattern.Patterns) 
            {
                var alternativeDesignPattern = new DesignPattern(chronoDesignPatterns);

                var alternativePatterns = patternsWithSameStartingLetter[pattern];

                var alternativeDesignPatterns = alternativePatterns.Select(x => new DesignPattern(alternativeDesignPattern.Patterns.Copy(), x));
                alternativeDesignPatterns = alternativeDesignPatterns.Where(x => x.Design.Length <= design.Colors.Length && design.Colors.Substring(0,x.Design.Length) == x.Design && !design.DesignPatternStarts.Any(y => y.PatternStart.IsEqual(x.Patterns))).ToList();

                design.DesignPatternStarts.AddRange(alternativeDesignPatterns.Select(x => new DesignPatternStart(x.Patterns.Copy())));

                var alternatives = alternativeDesignPatterns.Select(x=> new AlternativeDesignPatternToCheck(x, newDesignSubString.Substring(x.Design.Length)));
                alternativesToCheck.AddRange(alternatives);

                chronoDesignPatterns.Add(pattern);
                newDesignSubString.Substring(pattern.Length);
            }

            return alternativesToCheck;
        }

        private static void FindAlternativeDesigns(Design design, List<string> patterns, Dictionary<string, List<string>> patternsWithSameStartingLetter)
        {
            var originalDesignPattern = design.DesignPatterns.First();
            var newDesignPatterns = new List<DesignPattern> { originalDesignPattern };

            while (true)
            {
                var designPatternsToCheck = newDesignPatterns.ToList();
                newDesignPatterns = new List<DesignPattern>();

                foreach (var designPattern in designPatternsToCheck)
                {
                    var remainingDesignColors = design.Colors;
                    var chronoDesignPatterns = new List<string>();

                    foreach (var pattern in designPattern.Patterns)
                    {
                        var patternLength = pattern.Length;
                        var designSubstring = remainingDesignColors.Substring(patternLength);

                        var alternativePatterns = patternsWithSameStartingLetter[pattern];
                        var alternativeMatchingPatterns = FindMatchingPatterns(remainingDesignColors, alternativePatterns);
                        alternativeMatchingPatterns = RemovePreviouslyTriedAlternatives(alternativeMatchingPatterns, chronoDesignPatterns, design.DesignPatternStarts);

                        foreach (var alternativePattern in alternativeMatchingPatterns)
                        {
                            var alternativeDesignPattern = new DesignPattern();
                            alternativeDesignPattern.Patterns.AddRange(chronoDesignPatterns);
                            alternativeDesignPattern.Patterns.Add(alternativePattern);

                            design.DesignPatternStarts.Add(new DesignPatternStart(alternativeDesignPattern.Patterns.Copy()));

                            var newPatternLength = alternativePattern.Length;
                            var newDesignSubstring = remainingDesignColors.Substring(newPatternLength);

                            if (design.DesignPatternEndings.Any(x => x.PatternEndDesign.Equals(newDesignSubstring)))
                            {
                                var alternativeDesignPatternEndings = design.DesignPatternEndings.Where(x => x.PatternEndDesign == newDesignSubstring).ToList();

                                foreach (var alternativeDesignPatternEnding in alternativeDesignPatternEndings)
                                {
                                    var newDesignPattern = new DesignPattern();
                                    newDesignPattern.Patterns.AddRange(alternativeDesignPattern.Patterns);
                                    newDesignPattern.Patterns.AddRange(alternativeDesignPatternEnding.PatternEnd);

                                    if (!design.DesignPatterns.Includes(newDesignPattern))
                                    {
                                        newDesignPatterns.Add(newDesignPattern);
                                        design.DesignPatterns.Add(newDesignPattern);
                                    }
                                }

                                continue;
                            }

                            var newRemainingDesign = new Design(newDesignSubstring);

                            var canAlternativeDesignBeMade = CanDesignBeMade(newRemainingDesign, newRemainingDesign.Colors, patterns, new List<DesignAttempt>(), new DesignPattern(), new DesignPattern());

                            if (canAlternativeDesignBeMade)
                            {
                                alternativeDesignPattern.Patterns.AddRange(newRemainingDesign.DesignPatterns.First().Patterns);
                                if (!design.DesignPatterns.Includes(alternativeDesignPattern))
                                {
                                    newDesignPatterns.Add(alternativeDesignPattern);
                                    design.DesignPatterns.Add(alternativeDesignPattern);
                                    AddDesignPatternEndings(design, alternativeDesignPattern);
                                }
                            }
                        }

                        chronoDesignPatterns.Add(pattern);
                        remainingDesignColors = designSubstring;
                    }
                }

                if (designPatternsToCheck.Count == 0)
                {
                    break;
                }
            }
        }

        private static List<string> RemovePreviouslyTriedAlternatives(List<string> alternativePatterns, List<string> chronoDesignPatterns, List<DesignPatternStart> designPatternStarts)
        {
            var cleanedAlternatives = new List<string>();

            foreach (var alternativePattern in alternativePatterns)
            {
                var alternativeDesignPattern = new List<string>();
                alternativeDesignPattern.AddRange(chronoDesignPatterns);
                alternativeDesignPattern.Add(alternativePattern);

                if (!designPatternStarts.Any(x => x.PatternStart.IsEqual(alternativeDesignPattern)))
                {
                    cleanedAlternatives.Add(alternativePattern);
                }
            }

            return cleanedAlternatives;
        }
    }
}
