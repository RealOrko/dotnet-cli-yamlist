using System;
using System.IO;
using System.Text.RegularExpressions;
using yamlist.Modules.Commands.Options;
using yamlist.Modules.Commands.Routing;
using yamlist.Modules.Json;
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

        public string Input { get; set; }
        public StringWriter Out { get; set; }
        public CommandContext Context { get; }

        public int Execute(ToJsonArguments args)
        {
            var input = Input ?? File.ReadAllText(args.InputFile);
            var output = ForwardConverter.Transform(input);

            if (args.Debug)
            {
                var debugFile = Path.GetFileName(args.InputFile) + ".debug";
                File.WriteAllText(debugFile, output);
            }
            
            var json = Converter.ToJson(output);
            json = JsonConverter.Format(json);
            (Out ?? Console.Out).WriteLine(json);
            return 0;
        }
    }
}