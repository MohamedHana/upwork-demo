# Backend Dockerfile

# Step 1: Build Stage - Use the latest full Python 3.12-slim as the base image for building the application
FROM python:3.12-slim AS builder

# Step 2: Set the working directory inside the container
WORKDIR /app

# Step 3: Install build dependencies
RUN apt-get update && apt-get install -y --no-install-recommends \
    build-essential \
    && rm -rf /var/lib/apt/lists/*

# Step 4: Install Python dependencies
COPY requirements.txt ./
RUN pip install --no-cache-dir -r requirements.txt

# Step 5: Copy the rest of the application code
COPY . .

# Step 6: Set environment variables for Django
ENV PYTHONUNBUFFERED=1
ENV DJANGO_SETTINGS_MODULE=backend.settings
ENV DEBUG=${DEBUG}
ENV SECRET_KEY=${SECRET_KEY}
ENV ALLOWED_HOSTS=${ALLOWED_HOSTS}
ENV OPENAI_API_KEY=${OPENAI_API_KEY}
ENV OPENAI_ORGANIZATION_ID=${OPENAI_ORGANIZATION_ID}
ENV EMAIL_BACKEND=${EMAIL_BACKEND}
ENV EMAIL_HOST=${EMAIL_HOST}
ENV EMAIL_PORT=${EMAIL_PORT}
ENV EMAIL_USE_TLS=${EMAIL_USE_TLS}
ENV EMAIL_HOST_USER=${EMAIL_HOST_USER}
ENV EMAIL_HOST_PASSWORD=${EMAIL_HOST_PASSWORD}
ENV EMAIL_DEBUG=${EMAIL_DEBUG}

# Step 7: Collect static files (if your app has static files)
RUN python manage.py collectstatic --noinput

# Step 8: Run Django migrations
RUN python manage.py migrate

# Step 9: Final Stage - Use an even more minimal base image for the final container
FROM python:3.12-alpine AS final

# Step 10: Set the working directory inside the final container
WORKDIR /app

# Step 11: Copy only the necessary files from the build stage
COPY --from=builder /app /app

# Step 12: Install only runtime dependencies
COPY requirements.txt ./
RUN pip install --no-cache-dir --no-deps -r requirements.txt

# Step 13: Expose port 8000 to the outside world
EXPOSE 8000

# Step 14: Start the Django application using the minimal runtime image
CMD ["python", "manage.py", "runserver", "0.0.0.0:8000"]
