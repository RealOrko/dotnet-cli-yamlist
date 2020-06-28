# dotnet-cli-yamlist

## Format

For formatting concourse yaml files.

### Synopsis

```
yi fmt
------

   -file or -f
      The path to the concourse yaml file eg. -f ~/code/mypipeline/ci/deploy.yml
``` 

### Examples

Reformat yaml file:

```bash
yi fmt -f ~/code/mypipeline/ci/deploy.yml
yi fmt -f ~/code/mypipeline/ci/deploy.yml > newpipeline.yml
```
