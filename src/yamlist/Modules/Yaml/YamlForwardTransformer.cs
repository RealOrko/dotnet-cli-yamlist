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
                ForwardAnchorReplacement(ref currentLine);
                ForwardMergeIntoReplacement(ref currentLine, counter);
                ForwardAnchorUsageReplacement(ref currentLine, counter);
            }
        }

        private static void ForwardAnchorReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, @"(:\s*\&)");
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, @"(:\s*\&)", "___anchor___");
                currentLine = currentLine + ":";
            }
        }

        private static void ForwardMergeIntoReplacement(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, @"(<<:\s*\*)");
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, @"(<<:\s*\*)", $"___merge___{counter}___: ");
            }
        }

        private static void ForwardAnchorUsageReplacement(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, @"(?=[\ \t])(\s*\*)");
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, @"(?=[\ \t])(\s*\*)", $" ___call___{counter}___");
            }
        }
    }
}