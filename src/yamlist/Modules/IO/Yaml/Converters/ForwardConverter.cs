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
                AnchorCall(ref currentLine, counter);
            }
        }

        private static void AnchorDeclaration(ref string currentLine)
        {
            var match = Regex.Match(currentLine, SymbolMapper.AnchorDeclaration.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, SymbolMapper.AnchorDeclaration.Forward.FindRegEx, SymbolMapper.AnchorDeclaration.Forward.ReplaceWith);
                currentLine = currentLine + ":";
            }
        }

        private static void MergeAnchorDeclaration(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, SymbolMapper.MergeAnchorDeclaration.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, SymbolMapper.MergeAnchorDeclaration.Forward.FindRegEx, string.Format(SymbolMapper.MergeAnchorDeclaration.Forward.ReplaceWith, counter));
                currentLine = currentLine + ":";
            }
        }

        private static void AnchorCall(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, SymbolMapper.AnchorCall.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, SymbolMapper.AnchorCall.Forward.FindRegEx, string.Format(SymbolMapper.AnchorCall.Forward.ReplaceWith, counter));
            }
        }
        
        private static void MergeAnchorCall(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, SymbolMapper.MergeAnchorCall.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, SymbolMapper.MergeAnchorCall.Forward.FindRegEx, string.Format(SymbolMapper.MergeAnchorCall.Forward.ReplaceWith, counter));
            }
        }
    }
}