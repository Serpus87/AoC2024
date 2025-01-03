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
                var designCopy = new Design(design.Colors);
                var canDesignBeMade = CanDesignBeMade(design, designCopy, patterns, new List<DesignAttempt>(), new DesignPattern(), new DesignPattern());

                if (canDesignBeMade)
                {
                    design.CanBeMade = true;
                    design.DesignCounter++;
                    designsThatCanBeMade.Add(design);
                }
            }

            return designsThatCanBeMade;
        }

        private static bool CanDesignBeMade(Design originalDesign, Design remainingDesign, List<Pattern> patterns, List<DesignAttempt> designAttempts, DesignPattern originalDesignPattern, DesignPattern finalDesignPattern)
        {
            if (remainingDesign.Colors.Length == 0)
            {
                originalDesign.DesignPatterns.Add(finalDesignPattern);
                originalDesign.DesignAttempts.AddRange(designAttempts);

                return true;
            }

            var matchingPatterns = FindMatchingPatterns(remainingDesign, patterns);
            matchingPatterns = RemovePreviouslyAttemptedPatterns(matchingPatterns, originalDesign, remainingDesign, designAttempts);

            if (matchingPatterns.Count == 0 && remainingDesign.Colors == originalDesign.Colors)
            {
                return false;
            }

            if (matchingPatterns.Count == 0)
            {
                // add attempted pattern to attempted patterns
                var designAttemptSubstring = originalDesign.Colors.Substring(0, originalDesign.Colors.Length - remainingDesign.Colors.Length);
                designAttempts.Add(new DesignAttempt(designAttemptSubstring));

                var originalDesignCopy = new Design(originalDesign.Colors);
                var originalDesignPatternCopy = new DesignPattern();
                originalDesignPatternCopy.Patterns.AddRange(originalDesignPattern.Patterns);

                return CanDesignBeMade(originalDesign, originalDesignCopy, patterns, designAttempts, originalDesignPattern, originalDesignPatternCopy);
            }

            foreach (Pattern pattern in matchingPatterns)
            {
                var patternLength = pattern.Colors.Length;
                var designSubstring = remainingDesign.Colors.Substring(patternLength);
                var newRemainingDesign = new Design(designSubstring);
                finalDesignPattern.Patterns.Add(pattern);

                return CanDesignBeMade(originalDesign, newRemainingDesign, patterns, designAttempts, originalDesignPattern, finalDesignPattern);
            }

            return false;
        }

        private static List<Pattern> RemovePreviouslyAttemptedPatterns(List<Pattern> matchingPatterns, Design originalDesign, Design remainingDesign, List<DesignAttempt> designAttempts)
        {
            var cleanedMatchingPatterns = matchingPatterns.Copy();

            var finishedDesign = new Design(originalDesign.Colors.Substring(0, originalDesign.Colors.Length - remainingDesign.Colors.Length));

            foreach (var designAttempt in designAttempts)
            {
                if (designAttempt.Colors.Length + remainingDesign.Colors.Length < originalDesign.Colors.Length)
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

        private static List<Pattern> FindMatchingPatterns(Design design, List<Pattern> patterns)
        {
            var matchingPatterns = new List<Pattern>();

            foreach (Pattern pattern in patterns.Where(x => x.Colors.Length <= design.Colors.Length))
            {
                var patternLength = pattern.Colors.Length;

                var designSubstring = design.Colors.Substring(0, patternLength);

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
                //AddRemainingDesignPatternsThatCanBeFinished(design,design.DesignPatterns.First());
                //AddDesignPatternCombos(design,design.DesignPatterns.First());
                AddDesignPatternEndings(design,design.DesignPatterns.First());
                var designRelevantPatterns = GetDesignRelevantPatterns(design, patterns);
                FindAlternativeDesigns(design, designRelevantPatterns);
            }
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


                //designPatternEndings.Add(new DesignPatternEnding(newComboEnd));
                design.DesignPatternEndings.AddIfNew(new DesignPatternEnding(newComboEnd));
            }

            design.DesignPatternEndings.AddRange(designPatternEndings);
        }

        //private static void AddDesignPatternCombos(Design design, DesignPattern designPattern)
        //{
        //    var designPatternCombos = new List<DesignPatternCombo>();
        //    var comboStart = new List<Pattern>();
        //    var comboEnd = new List<Pattern>();
        //    comboEnd.AddRange(designPattern.Patterns);

        //    for (int i = 0; i < (designPattern.Patterns.Count - 1); i++)
        //    {
        //        comboStart.Add(designPattern.Patterns[i]);
        //        comboEnd.RemoveAt(0);

        //        var newComboStart = new List<Pattern>();
        //        newComboStart.AddRange(comboStart);
        //        var newComboEnd = new List<Pattern>();
        //        newComboEnd.AddRange(comboEnd);
                

        //        designPatternCombos.Add(new DesignPatternCombo(newComboStart, newComboEnd));
        //    }

        //    design.DesignPatternCombos.AddRange(designPatternCombos);
        //}

        //private static void AddRemainingDesignPatternsThatCanBeFinished(Design design, DesignPattern designPattern)
        //{
        //    var designPatternThatCanBeFinished = new DesignPattern();
        //    designPatternThatCanBeFinished.Patterns.AddRange(designPattern.Patterns);

        //    for (int i = (designPattern.Patterns.Count - 1); i > 0; i--) 
        //    {
        //        designPatternThatCanBeFinished.Patterns.RemoveAt(i);
        //        design.RemainingDesignPatternsThatCanBeFinished.Add(designPatternThatCanBeFinished);
        //    }
        //}

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

        private static void FindAlternativeDesigns(Design design, List<Pattern> patterns)
        {
            var originalDesignPattern = design.DesignPatterns.First();
            var newDesignPatterns = new List<DesignPattern> { originalDesignPattern };

            while (true)
            {
                var designPatternsToCheck = newDesignPatterns.ToList();
                newDesignPatterns = new List<DesignPattern>();

                foreach (var designPattern in designPatternsToCheck)
                {
                    var newDesignPatternsToAdd = new List<DesignPattern>();

                    var remainingDesign = new Design(design.Colors);
                    var chronoDesignPattern = new DesignPattern();

                    foreach (var pattern in designPattern.Patterns)
                    {
                        var patternLength = pattern.Colors.Length;
                        var designSubstring = remainingDesign.Colors.Substring(patternLength);

                        // get patterns with same starting letter
                        var alternativePatterns = GetPatternsWithSameStartingLetter(pattern, patterns);
                        var alternativeMatchingPatterns = FindMatchingPatterns(remainingDesign, alternativePatterns);
                        alternativeMatchingPatterns = RemovePreviouslyAttemptedPatterns(alternativeMatchingPatterns, design, remainingDesign, design.DesignAttempts);

                        // if pattern can be replaced, addIfNew to newDesignPatterns
                        foreach (var alternativePattern in alternativeMatchingPatterns)
                        {
                            var alternativeDesignPattern = new DesignPattern();
                            alternativeDesignPattern.Patterns.AddRange(chronoDesignPattern.Patterns);
                            alternativeDesignPattern.Patterns.Add(alternativePattern);

                            var newPatternLength = alternativePattern.Colors.Length;
                            var newDesignSubstring = remainingDesign.Colors.Substring(newPatternLength);

                            if (design.DesignPatternEndings.Any(x=>x.PatternEndDesign.Equals(newDesignSubstring))) 
                            {
                                var temp = new DesignPattern();
                                temp.Patterns.AddRange(alternativeDesignPattern.Patterns);

                                //var temp2 = design.DesignPatternCombos.First(x => x.ComboEndDesign == newDesignSubstring).ComboEnd;
                                var temp2 = design.DesignPatternEndings.First(x => x.PatternEndDesign == newDesignSubstring).PatternEnd;

                                temp.Patterns.AddRange(temp2);

                                if (!design.DesignPatterns.Includes(temp))
                                {
                                    newDesignPatterns.AddIfNew(temp);
                                    newDesignPatternsToAdd.AddIfNew(temp);
                                    //AddRemainingDesignPatternsThatCanBeFinished(design, temp); // todo maybe add if new
                                    //AddDesignPatternCombos(design, temp); // todo maybe add if new
                                    //design.DesignCounter++;
                                }

                                continue;
                            }

                            var newRemainingDesign = new Design(newDesignSubstring);
                            var newRemainingDesignCopy = new Design(newRemainingDesign.Colors);

                            var finishedDesign = alternativeDesignPattern.Patterns.Design();
                            var relevantDesignAttempts = GetRelevantDesignAttempts(design.DesignAttempts, finishedDesign);

                            var canAlternativeDesignBeMade = CanDesignBeMade(newRemainingDesign, newRemainingDesignCopy, patterns, relevantDesignAttempts, new DesignPattern(), new DesignPattern());

                            if (relevantDesignAttempts.Count != newRemainingDesign.DesignAttempts.Count)
                            {
                                var newDesignAttempts = GetNewDesignAttempts(newRemainingDesign.DesignAttempts, finishedDesign, design.DesignAttempts);
                                design.DesignAttempts.AddRange(newDesignAttempts);
                            }
                            
                            if (canAlternativeDesignBeMade)
                            {
                                alternativeDesignPattern.Patterns.AddRange(newRemainingDesign.DesignPatterns.First().Patterns);
                                if (!design.DesignPatterns.Includes(alternativeDesignPattern))
                                {
                                    newDesignPatterns.AddIfNew(alternativeDesignPattern);
                                    newDesignPatternsToAdd.AddIfNew(alternativeDesignPattern);
                                    //AddRemainingDesignPatternsThatCanBeFinished(design, alternativeDesignPattern); // todo maybe add if new
                                    AddDesignPatternEndings(design, alternativeDesignPattern); // todo maybe add if new 
                                }
                            }
                        }

                        chronoDesignPattern.Patterns.Add(pattern);
                        remainingDesign = new Design(designSubstring);
                    }

                    design.DesignPatterns.AddRange(newDesignPatternsToAdd);
                    design.DesignCounter += newDesignPatternsToAdd.Count;
                }

                if (designPatternsToCheck.Count == 0)
                {
                    break;
                }
            }
        }

        private static List<DesignAttempt> GetRelevantDesignAttempts(List<DesignAttempt> designAttempts, string finishedDesign)
        {
            var relevantDesignAttempts = new List<DesignAttempt>();

            foreach (var designAttempt in designAttempts)
            {
                var designAttemptSubstring = designAttempt.Colors.Substring(0, finishedDesign.Length);
                if (designAttemptSubstring == finishedDesign)
                {
                    var relevantDesignAttempt = new DesignAttempt(designAttempt.Colors.Substring(finishedDesign.Length));
                    relevantDesignAttempts.Add(relevantDesignAttempt);
                }
            }

            return relevantDesignAttempts;
        }

        private static List<DesignAttempt> GetNewDesignAttempts(List<DesignAttempt> designAttemptsFromAlternative, string finishedDesign, List<DesignAttempt> originalDesignAttempts)
        {
            var newDesignAttempts = new List<DesignAttempt>();

            foreach (var designAttempt in designAttemptsFromAlternative)
            {
                var newDesignAttempt = finishedDesign + designAttempt.Colors;
                if (!originalDesignAttempts.Select(x=>x.Colors).Contains(newDesignAttempt))
                {
                    newDesignAttempts.Add(new DesignAttempt(newDesignAttempt));
                }
            }

            return newDesignAttempts;
        }

        private static List<Pattern> GetPatternsWithSameStartingLetter(Pattern patternToReplace, List<Pattern> patterns)
        {
            var patternsWithSameStartingLetter = new List<Pattern>();

            foreach (var pattern in patterns)
            {
                if (pattern.Colors[0] == patternToReplace.Colors[0] && pattern.Colors != patternToReplace.Colors)
                {
                    patternsWithSameStartingLetter.Add(pattern);
                }
            }

            return patternsWithSameStartingLetter;
        }
    }
}
