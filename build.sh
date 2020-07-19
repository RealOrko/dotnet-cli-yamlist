#!/usr/bin/env bash

set -ex
set -o pipefail

rm -rf ./build
dotnet publish ./src/yamlist/ -o ./build/
mkdir ./build/simple-pipeline-with-task/
cp -r ./examples/simple-pipeline-with-task/* ./build/simple-pipeline-with-task/

