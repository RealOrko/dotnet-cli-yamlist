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
            ReverseAnchorReplacement(ref currentLine);
            ReverseMergeIntoReplacement(ref currentLine);
            ReverseAnchorUsageReplacement(ref currentLine);
            Reverse_v_1_1_yaml_multiline_block(ref currentLine);
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
        
        private static void Reverse_v_1_1_yaml_multiline_block(ref string currentLine)
        {
            var match = Regex.Match(currentLine, @"(:\s>)");
            if (match.Success)
            {
                currentLine = currentLine.TrimEnd();
                currentLine = Regex.Replace(currentLine, @"(:\s>)", ": |");
            }
        }
    }
}