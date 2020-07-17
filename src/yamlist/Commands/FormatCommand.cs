using System;
using System.IO;
using yamlist.Modules.Commands;
using yamlist.Modules.Commands.Attributes;
using yamlist.Modules.IO;

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
            var input = File.ReadAllText(args.InputFile);
            var concourse = Converter.ConcourseFormat(input, args.InputFile, args.Debug);
            Console.WriteLine(concourse);
            return 0;
        }

    }
}