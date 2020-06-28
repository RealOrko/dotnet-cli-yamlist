# dotnet-cli-yamlist

## To Json

For converting to concourse yaml to json.

### Synopsis

```
yi json
-------

   -file or -f
      The path to the concourse yaml file eg. -f ~/code/mypipeline/ci/deploy.yml

   -debug or -d
      To debug output eg -d
``` 

### Examples

Reformat yaml file:

```bash
yi json -f ~/code/mypipeline/ci/deploy.yml
yi json -f ~/code/mypipeline/ci/deploy.yml > newpipeline.json
yi json -f ~/code/mypipeline/ci/deploy.yml -d > newpipeline.json # creates deploy.json.debug
```
