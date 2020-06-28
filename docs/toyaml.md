# dotnet-cli-yamlist

## To Yaml

For converting to concourse json to yaml.

### Synopsis

```
yi yaml
-------

   -file or -f
      The path to the concourse yaml file eg. -f ~/code/mypipeline/ci/deploy.yml

   -debug or -d
      To debug output eg -d
``` 

### Examples

For converting json back to yaml:

```bash
yi yaml -f ~/code/mypipeline/ci/deploy.json
yi yaml -f ~/code/mypipeline/ci/deploy.json > newpipeline.yml
yi yaml -f ~/code/mypipeline/ci/deploy.json -d > newpipeline.yml # creates deploy.json.yaml
```
