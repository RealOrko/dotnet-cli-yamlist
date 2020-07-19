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
            args.JobName = $"{args.JobName}-{jobCount}";
            args.TaskName = $"{args.TaskName}-{jobCount}";
            
            FindOrCreateJob(pipeline, args.JobName, args.TaskName, args.TasksFolder);
            FindOrCreateGitResource(pipeline);
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
  - name: my-source-git

params:
  HELLO_WORLD:

run:
  path: bash
  args:
    - ""my-source-git/{args.TasksFolder}/{args.TaskName}/task.sh""
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

        private void FindOrCreateJob(Pipeline pipeline, string jobName, string taskName, string taskFolder)
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
                        Get = "my-source-git"
                    },
                    new JobPlan()
                    {
                        Task = taskName,
                        File = Path.Combine("my-source-git", taskFolder, taskName, $"{taskName}.yml"),
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

        private void FindOrCreateGitResource(Pipeline pipeline)
        {
            var newOrExistingGitResource = pipeline.Resources.FirstOrDefault(x => x.Name == "my-source-git");
            if (newOrExistingGitResource == null)
            {
                var newResource = new Resource()
                {
                    Name = "my-source-git",
                    Type = "git",
                    Source = new ResourceSource()
                    {
                        { "branch", "master" },
                        { "uri", "https://github.com/my-name/my-source-git.git" }
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