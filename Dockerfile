# Stage 1: The build environment, using the full .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["TransactionApi.csproj", "."]
RUN dotnet restore "./TransactionApi.csproj"

# Copy the rest of the source code and build the application
COPY . .
RUN dotnet build "TransactionApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "TransactionApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: The final, lightweight runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the port the app will listen on. App Runner uses 8080 by default.
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# The command to run the application
ENTRYPOINT ["dotnet", "TransactionApi.dll"]