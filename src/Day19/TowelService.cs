using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Day19.Models;
using AdventOfCode.Day19.Extensions;

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

        public static List<Design> FindAllPossibleDesigns(List<Design> designs, List<Pattern> patterns)
        {
            var allPossibleDesigns = new List<Design>();

            foreach (Design design in designs)
            {
                var designCopy = new Design(design.Colors);
                var canDesignBeMade = CanDesignBeMade(design, designCopy, patterns, new List<DesignAttempt>());

                if (canDesignBeMade)
                {
                    allPossibleDesigns.Add(design);
                }
            }

            return allPossibleDesigns;
        }

        private static bool CanDesignBeMade(Design originalDesign, Design remainingDesign, List<Pattern> patterns, List<DesignAttempt> designAttempts)
        {
            if (remainingDesign.Colors.Length == 0)
            {
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

                return CanDesignBeMade(originalDesign, originalDesign, patterns, designAttempts);
            }

            foreach (Pattern pattern in matchingPatterns)
            {
                var patternLength = pattern.Colors.Length;
                var designSubstring = remainingDesign.Colors.Substring(patternLength);
                var newRemainingDesign = new Design(designSubstring);

                return CanDesignBeMade(originalDesign, newRemainingDesign, patterns, designAttempts);
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
    }
}
