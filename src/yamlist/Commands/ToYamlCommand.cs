using System;
using System.IO;
using System.Text.RegularExpressions;
using yamlist.Modules.Commands.Options;
using yamlist.Modules.Commands.Routing;
using yamlist.Modules.Yaml;

namespace yamlist.Commands
{
    [Binds(typeof(ToYamlArguments))]
    public class ToYamlCommand
    {
        public ToYamlCommand(CommandContext context)
        {
            Context = context;
        }

        public string Input { get; set; }
        public StringWriter Out { get; set; }
        public CommandContext Context { get; }

        public int Execute(ToYamlArguments args)
        {
            var input = Input ?? File.ReadAllText(args.InputFile);
            var output = Converter.ToYaml(input);

            if (args.Debug)
            {
                var debugFile = Path.GetFileName(args.InputFile) + ".debug";
                File.WriteAllText(debugFile, output);
            }
            
            var json = ReverseConverter.Transform(output);
            (Out ?? Console.Out).WriteLine(json);
            return 0;
        }
    }
}