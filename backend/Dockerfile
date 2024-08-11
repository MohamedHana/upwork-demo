# Backend Dockerfile
# Step 1: Use the latest full Python 3.12 as the base image
FROM python:3.12

# Step 2: Set the working directory inside the container
WORKDIR /app

# Step 3: Install dependencies
COPY requirements.txt ./
RUN pip install --no-cache-dir -r requirements.txt

# Step 4: Copy the rest of the application code
COPY . .

# Step 5: Set environment variables for Django
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

# Create the STATIC_ROOT directory
RUN mkdir -p /app/staticfiles

# Step 6: Collect static files and run Django migrations
RUN python manage.py collectstatic --noinput
RUN python manage.py migrate

# Step 7: Expose port 8000 to the outside world
EXPOSE 8000

# Step 8: Start the Django development server
CMD ["python", "manage.py", "runserver", "0.0.0.0:8000"]
