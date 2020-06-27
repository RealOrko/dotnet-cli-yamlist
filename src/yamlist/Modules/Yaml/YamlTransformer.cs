using System.IO;
using System.Text.RegularExpressions;

namespace yamlist.Modules.Yaml
{
    public class YamlTransformer
    {
        public static string ForwardTransform(string input)
        {
            var reader = new StringReader(input);
            var writer = new StringWriter();

            var counter = 1;
            var currentLine = string.Empty;

            do
            {
                currentLine = reader.ReadLine();
                ForwardTransformLine(ref currentLine, counter);
                writer.WriteLine(currentLine);
                counter++;
            } while (currentLine != null);

            return writer.ToString();
        }

        public static string ReverseTransform(string input)
        {
            var reader = new StringReader(input);
            var writer = new StringWriter();

            var currentLine = string.Empty;

            do
            {
                currentLine = reader.ReadLine();
                ReverseTransformLine(ref currentLine);
                writer.WriteLine(currentLine);
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

        private static void ReverseTransformLine(ref string currentLine)
        {
            ReverseAnchorReplacement(ref currentLine);
            ReverseMergeIntoReplacement(ref currentLine);
            ReverseAnchorUsageReplacement(ref currentLine);
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
                currentLine = Regex.Replace(currentLine, @"(<<:\s*\*)", $"___merge_anchor_into_{counter}___: ");
            }
        }

        private static void ForwardAnchorUsageReplacement(ref string currentLine, int counter)
        {
            var match = Regex.Match(currentLine, @"(\s*\*)");
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, @"(\s*\*)", $" ___anchor_call_{counter}___");
            }
        }

        private static void ReverseAnchorReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, @"(___anchor___)");
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd().TrimEnd(':');
                currentLine = Regex.Replace(currentLine, @"(___anchor___)", ": &");
            }
        }

        private static void ReverseMergeIntoReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, @"(___merge_anchor_into_\d*___:\s)");
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, @"(___merge_anchor_into_\d*___:\s)", "<<: *");
            }
        }

        private static void ReverseAnchorUsageReplacement(ref string currentLine)
        {
            var match = Regex.Match(currentLine, @"(___anchor_call_\d*___)");
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, @"(___anchor_call_\d*___)", "*");
            }
        }
    }
}