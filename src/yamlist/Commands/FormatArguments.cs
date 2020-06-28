using yamlist.Modules.Commands.Options;
using yamlist.Modules.Commands.Options.Attributes;

namespace yamlist.Commands
{
    [Command("fmt")]
    public class FormatArguments
    {
        [Argument(ShortName = "-f", LongName = "-file", Help = "The path to the concourse yaml file eg. -f ~/code/mypipeline/ci/deploy.yml")]
        public string InputFile { get; set; }
    }
}