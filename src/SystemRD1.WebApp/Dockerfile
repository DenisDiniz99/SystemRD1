#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/SystemRD1.WebApp/SystemRD1.WebApp.csproj", "src/SystemRD1.WebApp/"]
RUN dotnet restore "src/SystemRD1.WebApp/SystemRD1.WebApp.csproj"
COPY . .
WORKDIR "/src/src/SystemRD1.WebApp"
RUN dotnet build "SystemRD1.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SystemRD1.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SystemRD1.WebApp.dll"]