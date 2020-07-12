using System.IO;
using System.Text.RegularExpressions;

namespace yamlist.Modules.IO.Yaml.Converters
{
    public class ForwardConverter
    {
        public static string Convert(string input)
        {
            var reader = new StringReader(input);
            var writer = new StringWriter();

            var counter = 1;
            var currentLine = string.Empty;

            do
            {
                currentLine = reader.ReadLine();
                if (string.IsNullOrEmpty(currentLine)) continue;
                ConvertLine(ref currentLine, counter);
                writer.WriteLine(currentLine);
                counter++;
            } while (currentLine != null);

            return writer.ToString();
        }

        private static void ConvertLine(ref string currentLine, int counter)
        {
            if (!currentLine.Contains("regexp:"))
            {
                MergeAnchorCall(ref currentLine, counter);
                MergeAnchorDeclaration(ref currentLine, counter);
                AnchorDeclaration(ref currentLine);
                AnchorListDeclaration(ref currentLine);
                AnchorCall(ref currentLine, counter);
            }
        }

        private static void AnchorDeclaration(ref string currentLine)
        {
            var anchorSymbol = SymbolMapper.AnchorDeclaration;
            var match = Regex.Match(currentLine, anchorSymbol.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, anchorSymbol.Forward.FindRegEx, anchorSymbol.Forward.ReplaceWith);
                currentLine = currentLine + ":";
            }
        }

        private static void AnchorListDeclaration(ref string currentLine)
        {
            var anchorListSymbol = SymbolMapper.AnchorListDeclaration;
            var match = Regex.Match(currentLine, anchorListSymbol.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, anchorListSymbol.Forward.FindRegEx, anchorListSymbol.Forward.ReplaceWith);
                currentLine = currentLine + ": 1";
            }
        }

        private static void MergeAnchorDeclaration(ref string currentLine, int counter)
        {
            var mergeAnchorSymbol = SymbolMapper.MergeAnchorDeclaration;
            var match = Regex.Match(currentLine, mergeAnchorSymbol.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, mergeAnchorSymbol.Forward.FindRegEx, string.Format(mergeAnchorSymbol.Forward.ReplaceWith, counter));
                currentLine = currentLine + ":";
            }
        }

        private static void AnchorCall(ref string currentLine, int counter)
        {
            var anchorCallSymbol = SymbolMapper.AnchorCall;
            var match = Regex.Match(currentLine, anchorCallSymbol.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, anchorCallSymbol.Forward.FindRegEx, string.Format(anchorCallSymbol.Forward.ReplaceWith, counter));
            }
        }
        
        private static void MergeAnchorCall(ref string currentLine, int counter)
        {
            var mergeAnchorCallSymbol = SymbolMapper.MergeAnchorCall;
            var match = Regex.Match(currentLine, mergeAnchorCallSymbol.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, mergeAnchorCallSymbol.Forward.FindRegEx, string.Format(mergeAnchorCallSymbol.Forward.ReplaceWith, counter));
            }
        }
    }
}