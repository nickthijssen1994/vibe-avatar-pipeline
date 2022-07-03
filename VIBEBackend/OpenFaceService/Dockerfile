FROM nickthijssen/openface-api:latest AS base
WORKDIR /code
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OpenFaceService/OpenFaceService.csproj", "OpenFaceService/"]
COPY ["MessagingNetwork/MessagingNetwork.csproj", "MessagingNetwork/"]
RUN dotnet restore "OpenFaceService/OpenFaceService.csproj"
COPY . .
WORKDIR "/src/OpenFaceService"
RUN dotnet build "OpenFaceService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenFaceService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /code
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OpenFaceService.dll"]
