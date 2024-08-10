#!/bin/bash

echo "Merging with branch 'development' and pushing to 'staging'"
git checkout staging
git merge development
git push
echo "Go back to branch 'development'"
git checkout development
