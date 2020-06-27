# dotnet-cli-yamlist

![Build](https://github.com/RealOrko/dotnet-cli-yamlist/workflows/Build/badge.svg)
![Publish](https://github.com/RealOrko/dotnet-cli-yamlist/workflows/Publish/badge.svg)

A tool that provides macro cli functions for working with yaml used in concourse pipelines.

## Why do I need this?

You work with relatively large yaml files consumed in concourse pipelines and you just can't be bothered
to spend months setting up your editor with functions that are hard to configure or simply do not exist. 

## Prerequisites

You need to install the [DOTNET Core SDK](https://dotnet.microsoft.com/download).

## Install

```
dotnet tool install -g dotnet-cli-yamlist
```

## Commands

Here are some examples and descriptions of all the possible commands. 
For any help please type the following command after installing: 

```bash
yi
```

If you would like help with a specific command just type

```bash
yi <command>
```

Available commands are:
 - [`todo`](https://github.com/RealOrko/dotnet-cli-yamlist/blob/master/docs/todo.md)

## Uninstall

```
dotnet tool uninstall -g dotnet-cli-yamlist
``` 
