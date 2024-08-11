#!/bin/bash

# Shut down docker app
echo "Updating the staging server..."
docker compose down

# Pull updates from the git repo
echo "Pulling from origin 'staging' branch"
git fetch
git pull
echo "Pulled latest updates on 'staging' branch"

# Turn on docker app
docker compose up --build -d