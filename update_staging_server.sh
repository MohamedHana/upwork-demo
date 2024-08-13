#!/bin/bash

# Shut down docker
echo "Shutting down docker..."
docker compose down

# Pull updates from the git repo
echo "Pulling from origin 'staging' branch"
git fetch
git pull
echo "Pulled latest updates on 'staging' branch"

# Clean docker 
# docker system prune -f

# Build docker containers
docker compose build backend
docker compose build frontend

# Turn on docker
docker compose up -d
