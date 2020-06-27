using yamlist.Modules.Commands.Options;

namespace yamlist.Commands
{
    [Command("json")]
    public class ToJsonArguments
    {
        [Argument(ShortName = "-f", LongName = "-file",
            Help = "The path to the concourse yaml file eg. -f ~/code/mypipeline/pipeline.yml")]
        public string InputFile { get; set; }
    }
}