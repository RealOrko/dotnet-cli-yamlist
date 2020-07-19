using System.Collections.Generic;
using System.IO;
using System.Linq;
using yamlist.Modules.Commands;
using yamlist.Modules.Commands.Attributes;
using yamlist.Modules.IO;
using yamlist.Modules.IO.Json.Model;

namespace yamlist.Commands
{
    [Binds(typeof(AddJobArguments))]
    public class AddJobCommand
    {
        public AddJobCommand(Context context)
        {
            Context = context;
        }

        public Context Context { get; }

        public int Execute(AddJobArguments args)
        {
            var pipeline = FindOrCreatePipeline(args.InputFile, args.Debug);
            var jobCount = FindExistingJobCount(pipeline, args.JobName);

            if (args.TasksFolder == null)
            {
                args.TasksFolder = $"{Path.GetDirectoryName(args.InputFile)}/tasks";
            }
            
            args.JobName = $"{args.JobName}-{jobCount}";
            args.TaskName = $"{args.TaskName}-{jobCount}";

            FindOrCreateJobGroup(pipeline, args.JobName, args.GroupName);
            FindOrCreateJob(pipeline, args.JobName, args.TaskName, args.TasksFolder, args.ResourceName);
            FindOrCreateGitResource(pipeline, args.ResourceName);
            FindOrCreateUbuntuResource(pipeline);

            if (!Directory.Exists(args.TasksFolder))
            {
                Directory.CreateDirectory(args.TasksFolder);
            }

            if (!Directory.Exists(Path.Combine(args.TasksFolder, args.TaskName)))
            {
                Directory.CreateDirectory(Path.Combine(args.TasksFolder, args.TaskName));
            }

            if (!File.Exists(Path.Combine(args.TasksFolder, args.TaskName, "task.yml")))
            {
                File.WriteAllText(Path.Combine(args.TasksFolder, args.TaskName, "task.yml"), $@"
platform: linux

inputs:
  - name: {args.ResourceName}

params:
  HELLO_WORLD:

run:
  path: bash
  args:
    - ""{args.ResourceName}/{args.TasksFolder}/{args.TaskName}/task.sh""
");
            }

            if (!File.Exists(Path.Combine(args.TasksFolder, args.TaskName, "task.sh")))
            {
                File.WriteAllText(Path.Combine(args.TasksFolder, args.TaskName, "task.sh"), $@"
#!/usr/bin/env bash

echo $HELLO_WORLD
");
            }
            
            var yaml = Converter.PipelineToYaml(pipeline, args.InputFile, args.Debug);
            File.WriteAllText(args.InputFile, yaml);
            
            return 0;
        }

        private void FindOrCreateJobGroup(Pipeline pipeline, string jobName, string groupName)
        {
            if (pipeline.Groups == null)
            {
                pipeline.Groups = new List<Group>();
            }

            if (!pipeline.Groups.Any(x => x.Name == groupName))
            {
                pipeline.Groups.Add(new Group()
                {
                    Name = groupName,
                    Jobs = new List<string>()
                });
            }

            var allGroup = pipeline.Groups.First(x => x.Name == groupName);

            if (allGroup.Jobs == null)
            {
                allGroup.Jobs = new List<string>();
            }
            
            allGroup.Jobs.Add(jobName);
        }

        private Pipeline FindOrCreatePipeline(string inputFile, bool debug)
        {
            if (!File.Exists(inputFile))
            {
                File.WriteAllText(string.Empty, inputFile);
                return new Pipeline()
                {
                    Jobs = new List<Job>(),
                    Resources = new List<Resource>()
                };
            }
            
            var pipelineYaml = File.ReadAllText(inputFile);
            var newOrExistingPipeline = Converter.ToConcourse(pipelineYaml, inputFile, debug);
            return newOrExistingPipeline;
        }

        private int FindExistingJobCount(Pipeline pipeline, string jobName)
        {
            var existingJobCount = pipeline.Jobs.Count(x => x.Name.StartsWith(jobName));
            return existingJobCount;
        }

        private void FindOrCreateJob(Pipeline pipeline, string jobName, string taskName, string taskFolder, string resourceName)
        {
            var newOrExistingjob = pipeline.Jobs.FirstOrDefault(x => x.Name == jobName);

            if (newOrExistingjob != null)
            {
                var existingJobCount = pipeline.Jobs.Count(x => x.Name.StartsWith(jobName));
                jobName = $"{jobName}_{existingJobCount}";
            }

            var newJob = new Job()
            {
                Name = jobName,
                Plan = new List<JobPlan>()
                {
                    new JobPlan()
                    {
                        Get = "ubuntu-image"
                    },
                    new JobPlan()
                    {
                        Get = resourceName
                    },
                    new JobPlan()
                    {
                        Task = taskName,
                        File = Path.Combine(resourceName, taskFolder, taskName, $"{taskName}.yml"),
                        Image = "ubuntu-image",
                        Params = new Dictionary<string, dynamic>()
                        {
                            {"HELLO_WORLD", "Hello world!"}
                        }
                    }
                }
            };

            pipeline.Jobs.Add(newJob);
        }

        private void FindOrCreateGitResource(Pipeline pipeline, string resourceName)
        {
            var newOrExistingGitResource = pipeline.Resources.FirstOrDefault(x => x.Name == resourceName);
            if (newOrExistingGitResource == null)
            {
                var newResource = new Resource()
                {
                    Name = resourceName,
                    Type = "git",
                    Source = new ResourceSource()
                    {
                        { "branch", "master" },
                        { "uri", $"https://github.com/my-name/{resourceName}.git" }
                    }
                };
                
                pipeline.Resources.Add(newResource);
            }
        }

        private void FindOrCreateUbuntuResource(Pipeline pipeline)
        {
            var newOrExistingGitResource = pipeline.Resources.FirstOrDefault(x => x.Name == "dotnet-cli-yamilst");
            if (newOrExistingGitResource == null)
            {
                var newResource = new Resource()
                {
                    Name = "ubuntu-image",
                    Type = "registry-image",
                    Source = new ResourceSource()
                    {
                        { "repository", "docker.io/ubuntu" }
                    }
                };
                
                pipeline.Resources.Add(newResource);
            }
        }
    }
}