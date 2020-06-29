using yamlist.Modules.Commands.Attributes;

namespace yamlist.Commands
{
    [Command("concourse")]
    public class ConcourseArguments
    {
        [Argument(ShortName = "-f", LongName = "-file", Help = "The path to the concourse yaml file eg. -f ~/code/mypipeline/ci/deploy.yml")]
        public string InputFile { get; set; }
    }
}