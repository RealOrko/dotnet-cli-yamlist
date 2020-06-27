#!/usr/bin/env bash

set -ex
set -o pipefail

dotnet publish src/yamlist/ -o build/
cp ../offline_cs_enterprise_archive/ci/deploy.yml ./build/d.yml
pushd ./build
./yi json -f d.yml > j.json
./yi yaml -f j.json > y.yml
meld d.yml y.yml
popd
