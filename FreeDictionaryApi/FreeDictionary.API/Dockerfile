#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://*:8000;http://*:80;
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FreeDictionary.API/FreeDictionary.API.csproj", "FreeDictionary.API/"]
COPY ["FreeDictionary.Application/FreeDictionary.Application.csproj", "FreeDictionary.Application/"]
COPY ["FreeDictionary.CrossCutting/FreeDictionary.CrossCutting.csproj", "FreeDictionary.CrossCutting/"]
COPY ["FreeDictionary.Data/FreeDictionary.Data.csproj", "FreeDictionary.Data/"]
COPY ["FreeDictionary.Domain/FreeDictionary.Domain.csproj", "FreeDictionary.Domain/"]
COPY ["FreeDictionary.Service/FreeDictionary.Service.csproj", "FreeDictionary.Service/"]
RUN dotnet restore "FreeDictionary.API/FreeDictionary.API.csproj"
COPY . .
WORKDIR "/src/FreeDictionary.API"
RUN dotnet build "FreeDictionary.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeDictionary.API.csproj" -c Release -o /app/publish /p:UseAppHost=false 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeDictionary.API.dll"]