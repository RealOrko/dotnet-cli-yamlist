using yamlist.Modules.Commands.Options;

namespace yamlist.Commands
{
    [Command("fmt")]
    public class FormatArguments
    {
        [Argument(ShortName = "-f", LongName = "-file", Help = "The path to the concourse yaml file eg. -f ~/code/mypipeline/pipeline.yml")]
        public string File { get; set; }
    }
}