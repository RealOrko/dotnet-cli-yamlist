using System;
using System.IO;
using yamlist.Modules.Commands.Options;
using yamlist.Modules.Commands.Routing;

namespace yamlist.Commands
{
    [Binds(typeof(FormatArguments))]
    public class FormatCommand
    {
        public FormatCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(FormatArguments args)
        {
            var jsonWriter = new StringWriter();
            var toJson = new ToJsonCommand(Context) {Out = jsonWriter};
            toJson.Execute(new ToJsonArguments() {InputFile = args.InputFile});
            
            var yamlWriter = new StringWriter();
            var toYaml = new ToYamlCommand(Context) {Out = yamlWriter};
            toYaml.Input = jsonWriter.ToString();
            toYaml.Execute(new ToYamlArguments());
            
            Console.WriteLine(yamlWriter.ToString());

            return 0;
        }
    }
}