using System;
using System.IO;
using System.Text.RegularExpressions;

namespace yamlist.Modules.Yaml
{
    public class YamlReverseTransformer
    {
        public static string Transform(string input)
        {
            var reader = new StringReader(input);
            var writer = new StringWriter();

            var currentLine = string.Empty;

            do
            {
                currentLine = reader.ReadLine();
                if (string.IsNullOrEmpty(currentLine)) continue;
                ReverseTransformLine(ref currentLine);
                writer.WriteLine(currentLine);
            } while (currentLine != null);

            return writer.ToString();
        }

        private static void ReverseTransformLine(ref string currentLine)
        {
            ReverseMergeAnchorUsageReplacement(ref currentLine);
            ReverseMergeAnchorReplacement(ref currentLine);
            ReverseAnchorReplacement(ref currentLine);
            ReverseAnchorUsageReplacement(ref currentLine);
            ReverseFoldToMultiline(ref currentLine);
            ReverseFoldToMultilineArray(ref currentLine);
        }

        private static void ReverseAnchorReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, YamlTransformerSymbols.Anchor.ReverseRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':');
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.Anchor.ReverseRegEx, YamlTransformerSymbols.Anchor.ReverseReplacement);
            }
        }

        private static void ReverseMergeAnchorReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, YamlTransformerSymbols.MergeAnchor.ReverseRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':');
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.MergeAnchor.ReverseRegEx, YamlTransformerSymbols.MergeAnchor.ReverseReplacement);
            }
        }

        private static void ReverseAnchorUsageReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, YamlTransformerSymbols.AnchorUsage.ReverseRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.AnchorUsage.ReverseRegEx, YamlTransformerSymbols.AnchorUsage.ReverseReplacement);
            }
        }
        
        private static void ReverseMergeAnchorUsageReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, YamlTransformerSymbols.MergeAnchorUsage.ReverseRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.MergeAnchorUsage.ReverseRegEx, YamlTransformerSymbols.MergeAnchorUsage.ReverseReplacement);
            }
        }
        
        private static void ReverseFoldToMultiline(ref string currentLine)
        {
            var match = Regex.Match(currentLine, @"(:\s>)");
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, @"(:\s>)", ": |");
            }
        }
        
        private static void ReverseFoldToMultilineArray(ref string currentLine)
        {
            var match = Regex.Match(currentLine, @"(-\s>)");
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, @"(-\s>)", "- |");
            }
        }

    }
}