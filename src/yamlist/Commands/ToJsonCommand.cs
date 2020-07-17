using System;
using System.IO;
using yamlist.Modules.Commands;
using yamlist.Modules.Commands.Attributes;
using yamlist.Modules.IO;

namespace yamlist.Commands
{
    [Binds(typeof(ToJsonArguments))]
    public class ToJsonCommand
    {
        public ToJsonCommand(Context context)
        {
            Context = context;
        }

        public Context Context { get; }

        public int Execute(ToJsonArguments args)
        {
            var input = File.ReadAllText(args.InputFile);
            var json = Converter.YamlToJson(input, args.InputFile, args.Debug);
            Console.WriteLine(json);
            return 0;
        }
    }
}