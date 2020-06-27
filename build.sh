#!/usr/bin/env bash

set -ex
set -o pipefail

#rm -rf ./build
dotnet publish ./src/yamlist/ -o ./build/
