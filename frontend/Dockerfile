# Frontend Dockerfile

# Step 1: Build Stage - Use a lightweight Node.js image for building the application
FROM node:18-alpine AS build-stage

# Step 2: Set the working directory inside the container
WORKDIR /app

# Step 3: Copy the package.json and package-lock.json files from the frontend folder
COPY package*.json ./

# Step 4: Install the dependencies
RUN npm install --no-cache

# Step 5: Copy the rest of the application code from the frontend folder
COPY . .

# Step 6: Build the Vue.js application with increased memory limit
RUN node --max-old-space-size=720 node_modules/.bin/vite build

# Step 7: Production Stage - Use a minimal Nginx image to serve the application
FROM nginx:alpine AS production-stage

# Step 8: Copy the build output from the previous stage
COPY --from=build-stage /app/dist /usr/share/nginx/html

# Step 9: Remove default Nginx configuration and add your custom config for SPA vue 3 app
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d

# Step 10: Expose port 80 to the outside world
EXPOSE 80

# Step 11: Start the Nginx server
CMD ["nginx", "-g", "daemon off;"]
