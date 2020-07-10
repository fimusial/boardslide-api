FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY ["Src/Server/Server.csproj", "Src/Server/"]
COPY ["Src/Application/Application.csproj", "Src/Application/"]
COPY ["Src/Domain/Domain.csproj", "Src/Domain/"]
COPY ["Src/Infrastructure/Infrastructure.csproj", "Src/Infrastructure/"]

RUN dotnet restore "Src/Server/Server.csproj"
COPY . .
WORKDIR "/src/Src/Server"
RUN dotnet build "Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoardSlide.API.Server.dll"]
