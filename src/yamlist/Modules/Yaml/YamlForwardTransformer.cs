using System;
using System.IO;
using System.Text.RegularExpressions;

namespace yamlist.Modules.Yaml
{
    public class YamlForwardTransformer
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
                ForwardTransformLine(ref currentLine, counter);
                writer.WriteLine(currentLine);
                counter++;
            } while (currentLine != null);

            return writer.ToString();
        }

        private static void ForwardTransformLine(ref string currentLine, int counter)
        {
            if (!currentLine.Contains("regexp:"))
            {
                ForwardMergeAnchorUsageReplacement(ref currentLine, counter);
                ForwardMergeAnchorReplacement(ref currentLine, counter);
                ForwardAnchorReplacement(ref currentLine);
                ForwardAnchorUsageReplacement(ref currentLine, counter);
            }
        }

        private static void ForwardAnchorReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, YamlTransformerSymbols.AnchorDecl.ForwardRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.AnchorDecl.ForwardRegEx, YamlTransformerSymbols.AnchorDecl.ForwardReplacement);
                currentLine = currentLine + ":";
            }
        }

        private static void ForwardMergeAnchorReplacement(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, YamlTransformerSymbols.MergeAnchorDecl.ForwardRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.MergeAnchorDecl.ForwardRegEx, string.Format(YamlTransformerSymbols.MergeAnchorDecl.ForwardReplacement, counter));
                currentLine = currentLine + ":";
            }
        }

        private static void ForwardAnchorUsageReplacement(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, YamlTransformerSymbols.AnchorCall.ForwardRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.AnchorCall.ForwardRegEx, string.Format(YamlTransformerSymbols.AnchorCall.ForwardReplacement, counter));
            }
        }
        
        private static void ForwardMergeAnchorUsageReplacement(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, YamlTransformerSymbols.MergeAnchorCall.ForwardRegEx);
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, YamlTransformerSymbols.MergeAnchorCall.ForwardRegEx, string.Format(YamlTransformerSymbols.MergeAnchorCall.ForwardReplacement, counter));
            }
        }
    }
}