#!/usr/bin/env bash

set -ex
set -o pipefail

dotnet publish src/yamlist/ -o build/
cp ../paas-azure-pcf-deployment/ci/deploy.yml ./build/d.yml
#cp ../delivery-azure-pipelines/ci/deploy_enterprise_archive.yml ./build/d.yml
#cp ../offline_cs_enterprise_archive/ci/deploy.yml ./build/d.yml #Fails because of yaml standard violations
pushd ./build
./yi json -f d.yml > j.json -d
./yi yaml -f j.json > y.yml -d
fly validate-pipeline -c y.yml
meld d.yml y.yml
popd