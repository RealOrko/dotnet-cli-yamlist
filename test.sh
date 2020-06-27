#!/usr/bin/env bash

set -e
set -o pipefail

cd ./build 

# Run console
./yi

echo "Passed!"