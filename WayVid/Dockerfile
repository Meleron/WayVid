FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR ./app
COPY . .

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["dotnet", "run", "WayVid.csproj"]