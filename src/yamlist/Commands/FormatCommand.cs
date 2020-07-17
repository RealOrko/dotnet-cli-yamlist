using System;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;
using yamlist.Modules.Commands;
using yamlist.Modules.Commands.Attributes;

namespace yamlist.Commands
{
    [Binds(typeof(FormatArguments))]
    public class FormatCommand
    {
        public FormatCommand(Context context)
        {
            Context = context;
        }

        public Context Context { get; }

        public int Execute(FormatArguments args)
        {
            var yamlWriter = FormatConcourse(args);

            var line = string.Empty;
            var cursor = new OutputCursor();
            var stringReader = new StringReader(yamlWriter.ToString());

            while (line != null)
            {
                if (line.TrimStart().StartsWith("anchors:"))
                {
                    cursor.MarkAnchors();
                    Console.WriteLine();
                }
                
                if (line.TrimStart().StartsWith("groups:"))
                {
                    cursor.MarkGroups();
                    Console.WriteLine();
                }
                
                if (line.TrimStart().StartsWith("jobs:"))
                {
                    cursor.MarkJobs();
                    Console.WriteLine();
                }
                
                if (line.TrimStart().StartsWith("resources:"))
                {
                    cursor.MarkResources();
                    Console.WriteLine();
                }
                
                if (line.TrimStart().StartsWith("resource_types:"))
                {
                    cursor.MarkResourceTypes();
                    Console.WriteLine();
                }

                if (!string.IsNullOrEmpty(line))
                {
                    Console.WriteLine(line);
                }

                line = stringReader.ReadLine();
            }
            
            Console.WriteLine(yamlWriter.ToString());

            return 0;
        }

        private StringWriter FormatConcourse(FormatArguments args)
        {
            var jsonWriter = new StringWriter();
            var toJson = new ToJsonCommand(Context) {Out = jsonWriter};
            toJson.Execute(new ToJsonArguments {InputFile = args.InputFile, Debug = args.Debug});

            var pipeline = Modules.IO.Json.Converter.JsonToConcourse(jsonWriter.ToString());
            var concourseJson = Modules.IO.Json.Converter.ConcourseToJson(pipeline);

            if (args.Debug)
            {
                File.WriteAllText($"{Path.GetFileName(args.InputFile)}.concoursetojson.debug", concourseJson);
            }

            var yamlWriter = new StringWriter();
            var toYaml = new ToYamlCommand(Context) {Out = yamlWriter};
            toYaml.Input = concourseJson;
            toYaml.Execute(new ToYamlArguments() {InputFile = args.InputFile, Debug = args.Debug});
            return yamlWriter;
        }

        private class OutputCursor
        {
            public bool IsInAnchors { private get; set; }
            public bool IsInGroups { private get; set; }
            public bool IsInJobs { private get; set; }
            public bool IsInResources { private get; set; }
            public bool IsInResourceTypes { private get; set; }

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