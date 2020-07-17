using System.IO;
using yamlist.Modules.IO.Yaml.Converters;

namespace yamlist.Modules.IO
{
    public class Converter
    {
        public static string ConcourseFormat(string input, string inputFile, bool debug)
        {
            var json = YamlToJson(input, inputFile, debug);
            var pipeline = Json.Converter.JsonToConcourse(json);
            var concourseJson = Json.Converter.ConcourseToJson(pipeline);

            if (debug)
            {
                File.WriteAllText($"{Path.GetFileName(inputFile)}.concoursetojson.debug", concourseJson);
            }

            var yaml = JsonToYaml(concourseJson, inputFile, debug);
            yaml = FormatConcourseOut(yaml);
            return yaml;
        }
        
        public static string YamlToJson(string input, string inputFile, bool debug)
        {
            var output = ForwardConverter.Convert(input);

            if (debug)
            {
                var debugFile = Path.GetFileName(inputFile) + ".tojson.debug";
                File.WriteAllText(debugFile, output);
            }

            var json = Yaml.Converter.ToJson(output);
            json =  Json.Converter.Format(json);
            return json;
        }
        
        public static string JsonToYaml(string input, string inputFile, bool debug)
        {
            var output = Yaml.Converter.ToYaml(input);

            if (debug)
            {
                var debugFile = Path.GetFileName(inputFile) + ".toyaml.debug";
                File.WriteAllText(debugFile, output);
            }

            var yaml = ReverseConverter.Convert(output);
            return yaml;
        }
        
        private static string FormatConcourseOut(string concourseYaml)
        {
            var line = string.Empty;
            var cursor = new OutputCursor();
            var stringReader = new StringReader(concourseYaml);
            var stringWriter = new StringWriter();
            
            while (line != null)
            {
                if (line.StartsWith("anchors:"))
                {
                    cursor.MarkAnchors();
                    stringWriter.WriteLine("anchors:");
                    line = stringReader.ReadLine();
                    continue;
                }

                if (line.StartsWith("groups:"))
                {
                    cursor.MarkGroups();
                    stringWriter.WriteLine();
                }

                if (line.StartsWith("jobs:"))
                {
                    cursor.MarkJobs();
                    stringWriter.WriteLine();
                }

                if (line.StartsWith("resources:"))
                {
                    cursor.MarkResources();
                    stringWriter.WriteLine();
                }

                if (line.StartsWith("resource_types:"))
                {
                    cursor.MarkResourceTypes();
                    stringWriter.WriteLine();
                }

                if (line.StartsWith("- name:"))
                {
                    stringWriter.WriteLine();
                }

                if (cursor.IsInAnchors && line.StartsWith("  ") && !line.StartsWith("   "))
                {
                    stringWriter.WriteLine();    
                }

                if (!string.IsNullOrEmpty(line))
                {
                    stringWriter.WriteLine(line);
                }

                line = stringReader.ReadLine();
            }

            return stringWriter.ToString();
        }

        private class OutputCursor
        {
            public bool IsInAnchors { get; private set; }
            public bool IsInGroups { get; private set; }
            public bool IsInJobs { get; private set; }
            public bool IsInResources { get; private set; }
            public bool IsInResourceTypes { get; private set; }

            public void MarkAnchors()
            {
                AllFalse();
                IsInAnchors = true;
            }

            public void MarkGroups()
            {
                AllFalse();
                IsInGroups = true;
            }

            public void MarkJobs()
            {
                AllFalse();
                IsInJobs = true;
            }

            public void MarkResources()
            {
                AllFalse();
                IsInResources = true;
            }

            public void MarkResourceTypes()
            {
                AllFalse();
                IsInResourceTypes = true;
            }

            private void AllFalse()
            {
                IsInAnchors = false;
                IsInGroups = false;
                IsInJobs = false;
                IsInResources = false;
                IsInResourceTypes = false;
            }
        }

    }
}