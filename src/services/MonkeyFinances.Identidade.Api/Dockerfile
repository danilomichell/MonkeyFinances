#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/MonkeyFinances.Identidade.Api/MonkeyFinances.Identidade.Api.csproj", "src/services/MonkeyFinances.Identidade.Api/"]
RUN dotnet restore "src/services/MonkeyFinances.Identidade.Api/MonkeyFinances.Identidade.Api.csproj"
COPY . .
WORKDIR "/src/src/services/MonkeyFinances.Identidade.Api"
RUN dotnet build "MonkeyFinances.Identidade.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MonkeyFinances.Identidade.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonkeyFinances.Identidade.Api.dll"]