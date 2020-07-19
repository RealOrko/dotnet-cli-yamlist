#!/usr/bin/env bash

# Needs to be run with sudo privs on fedora 32, not assuming this works on osx-64 at all. 

# Please run 'docker-compose up -d' from a fresh clone of https://github.com/RealOrko/concourse-docker first.

fly --target local-dev login -n main -c http://localhost:8080/ -u test -p test
fly -t local-dev intercept -j job-with-task/my-simple-job-with-task
