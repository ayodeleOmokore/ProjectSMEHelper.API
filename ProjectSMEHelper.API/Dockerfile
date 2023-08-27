#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProjectSMEHelper.API/ProjectSMEHelper.API.csproj", "ProjectSMEHelper.API/"]
RUN dotnet restore "ProjectSMEHelper.API/ProjectSMEHelper.API.csproj"
COPY . .
WORKDIR "/src/ProjectSMEHelper.API"
RUN dotnet build "ProjectSMEHelper.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectSMEHelper.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectSMEHelper.API.dll"]