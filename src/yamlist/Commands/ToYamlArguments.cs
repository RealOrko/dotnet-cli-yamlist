using yamlist.Modules.Commands.Attributes;

namespace yamlist.Commands
{
    [Command("yaml")]
    public class ToYamlArguments
    {
        [Argument(ShortName = "-f", LongName = "-file", Help = "The path to the concourse json file eg. -f ~/code/mypipeline/ci/deploy.json")]
        public string InputFile { get; set; }
        
        [Argument(ShortName = "-d", LongName = "-debug", Help = "To debug output eg -d")]
        public bool Debug { get; set; }
    }
}