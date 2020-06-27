#!/usr/bin/env bash

set -ex
set -o pipefail

dotnet publish src/yamlist/ -o build/
cp ../delivery-azure-pipelines/ci/deploy_enterprise_archive.yml build/d.yml
pushd ./build
./yi json -f d.yml > j.json
./yi yaml -f j.json > y.yml
meld d.yml y.yml
popd
