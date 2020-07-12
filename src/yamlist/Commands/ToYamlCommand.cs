using System;
using System.IO;
using System.Text.RegularExpressions;
using yamlist.Modules.Commands;
using yamlist.Modules.Commands.Attributes;
using yamlist.Modules.IO.Yaml;
using yamlist.Modules.IO.Yaml.Converters;
using Converter = yamlist.Modules.IO.Yaml.Converter;

namespace yamlist.Commands
{
    [Binds(typeof(ToYamlArguments))]
    public class ToYamlCommand
    {
        public ToYamlCommand(Context context)
        {
            Context = context;
        }

        public string Input { get; set; }
        public StringWriter Out { get; set; }
        public Context Context { get; }

        public int Execute(ToYamlArguments args)
        {
            var input = Input ?? File.ReadAllText(args.InputFile);
            var output = Converter.ToYaml(input);

            if (args.Debug)
            {
                var debugFile = Path.GetFileName(args.InputFile) + ".toyaml.debug";
                File.WriteAllText(debugFile, output);
            }
            
            var json = ReverseConverter.Convert(output);
            (Out ?? Console.Out).WriteLine(json);
            return 0;
        }
    }
}