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
            var match = Regex.Match(currentLine, YamlTransformerSymbols.AnchorDecl.Reverse.RegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':');
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.AnchorDecl.Reverse.RegEx, YamlTransformerSymbols.AnchorDecl.Reverse.Replacement);
            }
        }

        private static void ReverseMergeAnchorReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, YamlTransformerSymbols.MergeAnchorDecl.Reverse.RegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':');
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.MergeAnchorDecl.Reverse.RegEx, YamlTransformerSymbols.MergeAnchorDecl.Reverse.Replacement);
            }
        }

        private static void ReverseAnchorUsageReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, YamlTransformerSymbols.AnchorCall.Reverse.RegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.AnchorCall.Reverse.RegEx, YamlTransformerSymbols.AnchorCall.Reverse.Replacement);
            }
        }
        
        private static void ReverseMergeAnchorUsageReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, YamlTransformerSymbols.MergeAnchorCall.Reverse.RegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.MergeAnchorCall.Reverse.RegEx, YamlTransformerSymbols.MergeAnchorCall.Reverse.Replacement);
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