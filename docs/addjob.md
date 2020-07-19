# dotnet-cli-yamlist

## Add Job

For scaffolding a new job into concourse yaml files.

### Synopsis

```
yi addjob
---------

   -file or -f
      The path to the concourse yaml file eg. -f ~/code/mypipeline/ci/deploy.yml

   -jobname or -jn
      The job name to insert the task into (if not supplied 'my-new-job') eg. -jn my-pre-existing-job-name

   -taskname or -tn
      The name of the new task (if not supplied 'my-new-task' is assumed) eg. -tn hello-world 

   -taskfolder or -tf
      The for where the new task file is created (if not supplied './tasks' is assumed relative to pipeline) eg. -tf ~/code/mypipeline/ci/tasks

   -groupname or -gn
      The group name for the new job eg. -gn all

   -resourcename or -rn
      The resource name for the new job eg. -rn my-source-git

   -debug or -d
      To debug output eg -d
``` 

### Examples

Scaffold new job into yaml file:

```bash
yi addjob -f ~/code/mypipeline/ci/deploy.yml
```
