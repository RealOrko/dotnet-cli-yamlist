using System.IO;
using System.Text.RegularExpressions;

namespace yamlist.Modules.IO.Yaml
{
    public class ForwardConverter
    {
        public static string Transform(string input)
        {
            var reader = new StringReader(input);
            var writer = new StringWriter();

            var counter = 1;
            var currentLine = string.Empty;

            do
            {
                currentLine = reader.ReadLine();
                if (string.IsNullOrEmpty(currentLine)) continue;
                TransformLine(ref currentLine, counter);
                writer.WriteLine(currentLine);
                counter++;
            } while (currentLine != null);

            return writer.ToString();
        }

        private static void TransformLine(ref string currentLine, int counter)
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
            var match = Regex.Match(currentLine, TransformSymbols.AnchorDeclaration.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, TransformSymbols.AnchorDeclaration.Forward.FindRegEx, TransformSymbols.AnchorDeclaration.Forward.ReplaceWith);
                currentLine = currentLine + ":";
            }
        }

        private static void MergeAnchorDeclaration(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, TransformSymbols.MergeAnchorDeclaration.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, TransformSymbols.MergeAnchorDeclaration.Forward.FindRegEx, string.Format(TransformSymbols.MergeAnchorDeclaration.Forward.ReplaceWith, counter));
                currentLine = currentLine + ":";
            }
        }

        private static void AnchorCall(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, TransformSymbols.AnchorCall.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, TransformSymbols.AnchorCall.Forward.FindRegEx, string.Format(TransformSymbols.AnchorCall.Forward.ReplaceWith, counter));
            }
        }
        
        private static void MergeAnchorCall(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, TransformSymbols.MergeAnchorCall.Forward.FindRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, TransformSymbols.MergeAnchorCall.Forward.FindRegEx, string.Format(TransformSymbols.MergeAnchorCall.Forward.ReplaceWith, counter));
            }
        }
    }
}