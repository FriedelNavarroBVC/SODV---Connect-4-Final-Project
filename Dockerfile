# Start with a base image that includes PHP
FROM php:7.4-apache

# Copy your application's source code into the image
COPY src/ /var/www/html/

# Assuming `src/` is the directory containing your PHP code. 
# Adjust the path according to your project structure.

# Expose port 80 for the web server
EXPOSE 8080

# Set any environment variables needed by your application
ENV SOME_VARIABLE value

# The base image `php:7.4-apache` comes with a pre-configured Apache server
# that will serve your application.
