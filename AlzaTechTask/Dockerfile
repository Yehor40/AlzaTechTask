FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore 

# Copy everything else and build
COPY . ./
RUN dotnet publish  -o out

# Use the official .NET runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port 80 for the application
EXPOSE 8080
ENTRYPOINT ["dotnet", "AlzaTechTask.dll"]
