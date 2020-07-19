#!/usr/bin/env bash

# Needs to be run with sudo privs on fedora 32, not assuming this works on osx-64 at all. 

# Please run 'docker-compose up -d' from a fresh clone of https://github.com/RealOrko/concourse-docker first.

fly --target local-dev login -n main -c http://localhost:8080/ -u test -p test
fly -t local-dev sync
fly -t local-dev validate-pipeline -c ./pipeline.yml
fly -t local-dev set-pipeline -p local-dev -c ./pipeline.yml
fly -t local-dev unpause-pipeline -p local-dev
fly -t local-dev trigger-job -j local-dev/my-simple-job-with-task
