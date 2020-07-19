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
      The job name to insert eg. -jn my-new-job

   -taskname or -tn
      The name of the new task eg. -tn my-new-task 

   -taskfolder or -tf
      The folder for where the new task file eg. -tf ~/code/mypipeline/ci/tasks

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
