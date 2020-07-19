using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using yamlist.Modules.Commands;
using yamlist.Modules.Commands.Attributes;
using yamlist.Modules.IO;
using yamlist.Modules.IO.Json.Model;

namespace yamlist.Commands
{
    [Binds(typeof(AddTaskArguments))]
    public class AddTaskCommand
    {
        public AddTaskCommand(Context context)
        {
            Context = context;
        }

        public Context Context { get; }

        public int Execute(AddTaskArguments args)
        {
            var input = File.ReadAllText(args.InputFile);
            var pipeline = Converter.ToConcourse(input, args.InputFile, args.Debug);

            var job = FindOrCreateJob(pipeline, args.JobName, args.TaskName, args.TasksFolder);
            
            
            
            return 0;
        }

        private object FindOrCreateJob(Pipeline pipeline, string jobName, string taskName, string taskFolder)
        {
            var newOrExistingjob = pipeline.Jobs.FirstOrDefault(x => x.Name == jobName);
            if (newOrExistingjob == null)
            {
                return new Job()
                {
                    Name = jobName,
                    Plan = new List<JobPlan>()
                    {
                        new JobPlan()
                        {
                            Task = taskName,
                            File = Path.Combine(taskFolder, $"{taskName}.yml"),
                            Image = "ubuntu"
                        }
                    }
                };
            }

            return newOrExistingjob;
        }
    }
}