using System;
using System.IO;
using System.Text.RegularExpressions;
using yamlist.Modules.Commands.Options;
using yamlist.Modules.Commands.Routing;
using yamlist.Modules.Yaml;

namespace yamlist.Commands
{
    [Binds(typeof(ToJsonArguments))]
    public class ToJsonCommand
    {
        public ToJsonCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(ToJsonArguments args)
        {
            var input = File.ReadAllText(args.InputFile);
            var output = YamlForwardTransformer.Transform(input);
            var json = YamlConverter.ToJson(output);
            Console.WriteLine(json);
            return 0;
        }
    }
}