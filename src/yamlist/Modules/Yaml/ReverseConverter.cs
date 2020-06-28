using System;
using System.IO;
using System.Text.RegularExpressions;

namespace yamlist.Modules.Yaml
{
    public class ReverseConverter
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
                TransformLine(ref currentLine);
                writer.WriteLine(currentLine);
            } while (currentLine != null);

            return writer.ToString();
        }

        private static void TransformLine(ref string currentLine)
        {
            MergeAnchorCall(ref currentLine);
            MergeAnchorDeclaration(ref currentLine);
            AnchorDeclaration(ref currentLine);
            AnchorCall(ref currentLine);
            FoldToMultiline(ref currentLine);
            FoldToMultilineArray(ref currentLine);
        }

        private static void AnchorDeclaration(ref string currentLine)
        {
            var match = Regex.Match(currentLine, TransformSymbols.AnchorDeclaration.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':');
                currentLine = Regex.Replace(currentLine, TransformSymbols.AnchorDeclaration.Reverse.FindRegEx, TransformSymbols.AnchorDeclaration.Reverse.ReplaceWith);
            }
        }

        private static void MergeAnchorDeclaration(ref string currentLine)
        {
            var match = Regex.Match(currentLine, TransformSymbols.MergeAnchorDeclaration.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':');
                currentLine = Regex.Replace(currentLine, TransformSymbols.MergeAnchorDeclaration.Reverse.FindRegEx, TransformSymbols.MergeAnchorDeclaration.Reverse.ReplaceWith);
            }
        }

        private static void AnchorCall(ref string currentLine)
        {
            var match = Regex.Match(currentLine, TransformSymbols.AnchorCall.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, TransformSymbols.AnchorCall.Reverse.FindRegEx, TransformSymbols.AnchorCall.Reverse.ReplaceWith);
            }
        }
        
        private static void MergeAnchorCall(ref string currentLine)
        {
            var match = Regex.Match(currentLine, TransformSymbols.MergeAnchorCall.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, TransformSymbols.MergeAnchorCall.Reverse.FindRegEx, TransformSymbols.MergeAnchorCall.Reverse.ReplaceWith);
            }
        }
        
        private static void FoldToMultiline(ref string currentLine)
        {
            var match = Regex.Match(currentLine, @"(:\s>)");
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, @"(:\s>)", ": |");
            }
        }
        
        private static void FoldToMultilineArray(ref string currentLine)
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