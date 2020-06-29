using System;
using System.IO;
using yamlist.Modules.Commands;
using yamlist.Modules.Commands.Attributes;

namespace yamlist.Commands
{
    [Binds(typeof(ConcourseArguments))]
    public class ConcourseCommand
    {
        public ConcourseCommand(Context context)
        {
            Context = context;
        }

        public Context Context { get; }

        public int Execute(ConcourseArguments args)
        {
            var jsonWriter = new StringWriter();
            var toJson = new ToJsonCommand(Context) {Out = jsonWriter};
            toJson.Execute(new ToJsonArguments {InputFile = args.InputFile});

            var pipeline = yamlist.Modules.IO.Yaml.Converter.JsonToConcourse(jsonWriter.ToString());
            var concourseJson = yamlist.Modules.IO.Yaml.Converter.ConcourseToJson(pipeline);

            var yamlWriter = new StringWriter();
            var toYaml = new ToYamlCommand(Context) {Out = yamlWriter};
            toYaml.Input = concourseJson;
            toYaml.Execute(new ToYamlArguments());

            Console.WriteLine(yamlWriter.ToString());

            return 0;
        }
    }
}