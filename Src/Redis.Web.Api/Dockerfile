#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/Redis.Web.Api/Redis.Web.Api.csproj", "Src/Redis.Web.Api/"]
RUN dotnet restore "Src/Redis.Web.Api/Redis.Web.Api.csproj"
COPY . .
WORKDIR "/src/Src/Redis.Web.Api"
RUN dotnet build "Redis.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Redis.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Redis.Web.Api.dll"]
