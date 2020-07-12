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
            AnchorListDeclaration(ref currentLine);
            AnchorDeclaration(ref currentLine);
            AnchorCall(ref currentLine);
            FoldToMultiline(ref currentLine);
            FoldToMultilineArray(ref currentLine);
        }

        private static void AnchorDeclaration(ref string currentLine)
        {
            var anchorDeclarationSymbol = SymbolMapper.AnchorDeclaration;
            var match = Regex.Match(currentLine, anchorDeclarationSymbol.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':');
                currentLine = Regex.Replace(currentLine, anchorDeclarationSymbol.Reverse.FindRegEx, anchorDeclarationSymbol.Reverse.ReplaceWith);
            }
        }

        private static void AnchorListDeclaration(ref string currentLine)
        {
            var anchorListDeclarationSymbol = SymbolMapper.AnchorListDeclaration;
            var match = Regex.Match(currentLine, anchorListDeclarationSymbol.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':', ' ', '1');
                currentLine = Regex.Replace(currentLine, anchorListDeclarationSymbol.Reverse.FindRegEx, anchorListDeclarationSymbol.Reverse.ReplaceWith);
            }
        }

        private static void MergeAnchorDeclaration(ref string currentLine)
        {
            var mergeAnchorDeclarationSymbol = SymbolMapper.MergeAnchorDeclaration;
            var match = Regex.Match(currentLine, mergeAnchorDeclarationSymbol.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':');
                currentLine = Regex.Replace(currentLine, mergeAnchorDeclarationSymbol.Reverse.FindRegEx, mergeAnchorDeclarationSymbol.Reverse.ReplaceWith);
            }
        }

        private static void AnchorCall(ref string currentLine)
        {
            var anchorCallSymbol = SymbolMapper.AnchorCall;
            var match = Regex.Match(currentLine, anchorCallSymbol.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, anchorCallSymbol.Reverse.FindRegEx, anchorCallSymbol.Reverse.ReplaceWith);
            }
        }
        
        private static void MergeAnchorCall(ref string currentLine)
        {
            var mergeAnchorCallSymbol = SymbolMapper.MergeAnchorCall;
            var match = Regex.Match(currentLine, mergeAnchorCallSymbol.Reverse.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, mergeAnchorCallSymbol.Reverse.FindRegEx, mergeAnchorCallSymbol.Reverse.ReplaceWith);
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