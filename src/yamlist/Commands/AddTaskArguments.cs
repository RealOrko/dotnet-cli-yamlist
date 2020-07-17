using yamlist.Modules.Commands.Attributes;

namespace yamlist.Commands
{
    [Command("addtask")]
    public class AddTaskArguments
    {
        [Argument(ShortName = "-f", LongName = "-file", Help = "The path to the concourse yaml file eg. -f ~/code/mypipeline/ci/deploy.yml")]
        public string InputFile { get; set; }

        [Argument(ShortName = "-jn", LongName = "-jobname", Help = "The job name to insert the task into (if not supplied 'my-new-job') eg. -jn my-pre-existing-job-name")]
        public string JobName { get; set; } = "my-new-job";
        
        [Argument(ShortName = "-tn", LongName = "-taskname", Help = "The name of the new task (if not supplied 'my-new-task' is assumed) eg. -tn hello-world ")]
        public string TaskName { get; set; } = "my-new-task";
        
        [Argument(ShortName = "-tf", LongName = "-taskfolder", Help = "The for where the new task file is created (if not supplied './tasks' is assumed relative to pipeline) eg. -tf ~/code/mypipeline/ci/tasks")]
        public string TasksFolder { get; set; } = "./tasks";

        [Argument(ShortName = "-d", LongName = "-debug", Help = "To debug output eg -d")]
        public bool Debug { get; set; }
    }
}