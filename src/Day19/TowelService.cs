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

        public static List<Pattern> GetPatterns(string input)
        {
            var patterns = new List<Pattern>();
            var pattern = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ',' || input[i] == ' ')
                {
                    if (pattern != string.Empty)
                    {
                        patterns.Add(new Pattern(pattern));
                        pattern = string.Empty;
                    }
                    continue;
                }
                pattern = pattern + input[i];
            }

            patterns.Add(new Pattern(pattern));

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

        public static List<Design> FindDesignsThatCanBeMade(List<Design> designs, List<Pattern> patterns)
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

        private static bool CanDesignBeMade(Design originalDesign, string remainingDesignColors, List<Pattern> patterns, List<DesignAttempt> designAttempts, DesignPattern originalDesignPattern, DesignPattern finalDesignPattern)
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

            foreach (Pattern pattern in matchingPatterns)
            {
                var patternLength = pattern.Colors.Length;
                var newRemainingDesign = remainingDesignColors.Substring(patternLength);
                finalDesignPattern.Patterns.Add(pattern);

                return CanDesignBeMade(originalDesign, newRemainingDesign, patterns, designAttempts, originalDesignPattern, finalDesignPattern);
            }

            return false;
        }

        private static List<Pattern> RemovePreviouslyAttemptedPatterns(List<Pattern> matchingPatterns, Design originalDesign, string remainingDesignColors, List<DesignAttempt> designAttempts)
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
                    if (finishedDesign.Colors + machtingPattern.Colors == designAttempt.Colors)
                    {
                        cleanedMatchingPatterns = cleanedMatchingPatterns.Where(x => x.Colors != machtingPattern.Colors).ToList();
                    }
                }
            }

            return cleanedMatchingPatterns;
        }

        private static List<Pattern> FindMatchingPatterns(string designColors, List<Pattern> patterns)
        {
            var matchingPatterns = new List<Pattern>();

            foreach (Pattern pattern in patterns.Where(x => x.Colors.Length <= designColors.Length))
            {
                var patternLength = pattern.Colors.Length;

                var designSubstring = designColors.Substring(0, patternLength);

                if (designSubstring == pattern.Colors)
                {
                    matchingPatterns.Add(pattern);
                }
            }

            return matchingPatterns;
        }

        public static void FindAlternativeDesignsForDesignsThatCanBeMade(List<Design> designsThatCanBeMade, List<Pattern> patterns)
        {
            foreach (var design in designsThatCanBeMade)
            {
                AddDesignPatternEndings(design, design.DesignPatterns.First());
                var designRelevantPatterns = GetDesignRelevantPatterns(design, patterns);
                var patternsWithSameStartingLetter = GetPatternsWithSameStartingLetter(designRelevantPatterns);
                FindAlternativeDesigns(design, designRelevantPatterns, patternsWithSameStartingLetter);
            }
        }

        private static Dictionary<string, List<Pattern>> GetPatternsWithSameStartingLetter(List<Pattern> designRelevantPatterns)
        {
            var patternsWithSameStartingLetter = new Dictionary<string, List<Pattern>>();

            foreach (var pattern in designRelevantPatterns)
            {
                patternsWithSameStartingLetter.Add(pattern.Colors, designRelevantPatterns.Where(x => x.Colors[0] == pattern.Colors[0] && x.Colors != pattern.Colors).ToList());
            }

            return patternsWithSameStartingLetter;
        }

        private static void AddDesignPatternEndings(Design design, DesignPattern designPattern)
        {
            var designPatternEndings = new List<DesignPatternEnding>();
            var comboEnd = new List<Pattern>();
            comboEnd.AddRange(designPattern.Patterns);

            for (int i = 0; i < (designPattern.Patterns.Count - 1); i++)
            {
                comboEnd.RemoveAt(0);

                var newComboEnd = new List<Pattern>();
                newComboEnd.AddRange(comboEnd);

                design.DesignPatternEndings.AddIfNew(new DesignPatternEnding(newComboEnd));
            }
        }

        private static List<Pattern> GetDesignRelevantPatterns(Design design, List<Pattern> patterns)
        {
            var relevantPatterns = new List<Pattern>();

            foreach (var pattern in patterns)
            {
                if (design.Colors.Contains(pattern.Colors))
                {
                    relevantPatterns.Add(pattern);
                }
            }

            return relevantPatterns;
        }

        private static void FindAlternativeDesigns(Design design, List<Pattern> patterns, Dictionary<string, List<Pattern>> patternsWithSameStartingLetter)
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
                    var chronoDesignPatterns = new List<Pattern>();

                    foreach (var pattern in designPattern.Patterns)
                    {
                        var patternLength = pattern.Colors.Length;
                        var designSubstring = remainingDesignColors.Substring(patternLength);

                        var alternativePatterns = patternsWithSameStartingLetter[pattern.Colors];
                        var alternativeMatchingPatterns = FindMatchingPatterns(remainingDesignColors, alternativePatterns);
                        alternativeMatchingPatterns = RemovePreviouslyTriedAlternatives(alternativeMatchingPatterns, chronoDesignPatterns, design.DesignPatternStarts);

                        foreach (var alternativePattern in alternativeMatchingPatterns)
                        {
                            var alternativeDesignPattern = new DesignPattern();
                            alternativeDesignPattern.Patterns.AddRange(chronoDesignPatterns);
                            alternativeDesignPattern.Patterns.Add(alternativePattern);

                            design.DesignPatternStarts.Add(new DesignPatternStart(alternativeDesignPattern.Patterns.Copy()));

                            var newPatternLength = alternativePattern.Colors.Length;
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

        private static List<Pattern> RemovePreviouslyTriedAlternatives(List<Pattern> alternativePatterns, List<Pattern> chronoDesignPatterns, List<DesignPatternStart> designPatternStarts)
        {
            var cleanedAlternatives = new List<Pattern>();

            foreach (var alternativePattern in alternativePatterns)
            {
                var alternativeDesignPattern = new List<Pattern>();
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
