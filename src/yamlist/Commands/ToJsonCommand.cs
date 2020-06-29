using System;
using System.IO;
using System.Text.RegularExpressions;
using yamlist.Modules.Commands;
using yamlist.Modules.Commands.Attributes;
using yamlist.Modules.IO.Yaml;
using yamlist.Modules.IO.Yaml.Transformers;
using Converter = yamlist.Modules.IO.Json.Converter;

namespace yamlist.Commands
{
    [Binds(typeof(ToJsonArguments))]
    public class ToJsonCommand
    {
        public ToJsonCommand(Context context)
        {
            Context = context;
        }

        public string Input { get; set; }
        public StringWriter Out { get; set; }
        public Context Context { get; }

        public int Execute(ToJsonArguments args)
        {
            var input = Input ?? File.ReadAllText(args.InputFile);
            var output = ForwardConverter.Transform(input);

            if (args.Debug)
            {
                var debugFile = Path.GetFileName(args.InputFile) + ".debug";
                File.WriteAllText(debugFile, output);
            }
            
            var json = Modules.IO.Yaml.Converter.ToJson(output);
            json = Converter.Format(json);
            (Out ?? Console.Out).WriteLine(json);
            return 0;
        }
    }
}