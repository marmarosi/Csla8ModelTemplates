# Pull the latest SQL Server image.
FROM mcr.microsoft.com/mssql/server:2022-latest

# Create app directory.
WORKDIR /app

# Copy initialization scripts.
COPY . /app

# Specify environment variables for SQL Server.
ENV ACCEPT_EULA Y
ENV MSSQL_SA_PASSWORD MWGfWg4lid2lL2I0

# Expose port 1433 in case accessing from other container.
# Expose port externally from docker-compose.yml.
EXPOSE 1433

# Run Microsoft SQL Server and initialization script (at the same time).
CMD /bin/bash ./entrypoint.sh
