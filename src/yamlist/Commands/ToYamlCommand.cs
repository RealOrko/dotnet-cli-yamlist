using System;
using System.IO;
using yamlist.Modules.Commands;
using yamlist.Modules.Commands.Attributes;
using yamlist.Modules.IO;

namespace yamlist.Commands
{
    [Binds(typeof(ToYamlArguments))]
    public class ToYamlCommand
    {
        public ToYamlCommand(Context context)
        {
            Context = context;
        }

        public Context Context { get; }

        public int Execute(ToYamlArguments args)
        {
            var input = File.ReadAllText(args.InputFile);
            var yaml = Converter.JsonToYaml(input, args.InputFile, args.Debug);
            Console.WriteLine(yaml);
            return 0;
        }
    }
}