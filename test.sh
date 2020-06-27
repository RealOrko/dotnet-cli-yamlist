#!/usr/bin/env bash

set -ex
set -o pipefail

cd ./build 

# Run console
./yi

echo "Passed!"