using System.IO;
using System.Text.RegularExpressions;

namespace yamlist.Modules.IO.Yaml.Converters
{
    public class ReverseConverter
    {
        public static string Convert(string input)
        {
            var reader = new StringReader(input);
            var writer = new StringWriter();

            var currentLine = string.Empty;

            do
            {
                currentLine = reader.ReadLine();
                if (string.IsNullOrEmpty(currentLine)) continue;
                ConvertLine(ref currentLine);
                writer.WriteLine(currentLine);
            } while (currentLine != null);

            return writer.ToString();
        }

        private static void ConvertLine(ref string currentLine)
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
            var match = Regex.Match(currentLine, SymbolMapper.AnchorDeclaration.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':');
                currentLine = Regex.Replace(currentLine, SymbolMapper.AnchorDeclaration.Reverse.FindRegEx, SymbolMapper.AnchorDeclaration.Reverse.ReplaceWith);
            }
        }

        private static void MergeAnchorDeclaration(ref string currentLine)
        {
            var match = Regex.Match(currentLine, SymbolMapper.MergeAnchorDeclaration.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':');
                currentLine = Regex.Replace(currentLine, SymbolMapper.MergeAnchorDeclaration.Reverse.FindRegEx, SymbolMapper.MergeAnchorDeclaration.Reverse.ReplaceWith);
            }
        }

        private static void AnchorCall(ref string currentLine)
        {
            var match = Regex.Match(currentLine, SymbolMapper.AnchorCall.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, SymbolMapper.AnchorCall.Reverse.FindRegEx, SymbolMapper.AnchorCall.Reverse.ReplaceWith);
            }
        }
        
        private static void MergeAnchorCall(ref string currentLine)
        {
            var match = Regex.Match(currentLine, SymbolMapper.MergeAnchorCall.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, SymbolMapper.MergeAnchorCall.Reverse.FindRegEx, SymbolMapper.MergeAnchorCall.Reverse.ReplaceWith);
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