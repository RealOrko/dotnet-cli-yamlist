using yamlist.Modules.Commands.Attributes;

namespace yamlist.Commands
{
    [Command("addjob")]
    public class AddJobArguments
    {
        [Argument(ShortName = "-f", LongName = "-file", Help = "The path to the concourse yaml file eg. -f ~/code/mypipeline/ci/deploy.yml")]
        public string InputFile { get; set; } = "pipeline.yml";

        [Argument(ShortName = "-jn", LongName = "-jobname", Help = "The job name to insert eg. -jn my-new-job")]
        public string JobName { get; set; } = "my-new-job";
        
        [Argument(ShortName = "-tn", LongName = "-taskname", Help = "The name of the new task eg. -tn my-new-task ")]
        public string TaskName { get; set; } = "my-new-task";
        
        [Argument(ShortName = "-tf", LongName = "-taskfolder", Help = "The folder for where the new task file eg. -tf ~/code/mypipeline/ci/tasks")]
        public string TasksFolder { get; set; }

        [Argument(ShortName = "-gn", LongName = "-groupname", Help = "The group name for the new job eg. -gn all")]
        public string GroupName { get; set; } = "all";

        [Argument(ShortName = "-rn", LongName = "-resourcename", Help = "The resource name for the new job eg. -rn my-source-git")]
        public string ResourceName { get; set; } = "my-source-git";

        [Argument(ShortName = "-d", LongName = "-debug", Help = "To debug output eg -d")]
        public bool Debug { get; set; }
    }
}