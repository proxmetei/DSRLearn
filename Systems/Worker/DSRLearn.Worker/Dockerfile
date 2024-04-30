#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Worker/DSRLearn.Worker/DSRLearn.Worker.csproj", "Systems/Worker/DSRLearn.Worker/"]
COPY ["Services/DSRLearn.Services.Actions/DSRLearn.Services.Actions.csproj", "Services/DSRLearn.Services.Actions/"]
COPY ["Services/DSRLearn.Services.RabbitMq/DSRLearn.Services.RabbitMq.csproj", "Services/DSRLearn.Services.RabbitMq/"]
COPY ["Shared/DSRLearn.Common/DSRLearn.Common.csproj", "Shared/DSRLearn.Common/"]
COPY ["Services/DSRLearn.Services.Logger/DSRLearn.Services.Logger.csproj", "Services/DSRLearn.Services.Logger/"]
COPY ["Services/DSRLearn.Services.Settings/DSRLearn.Services.Settings.csproj", "Services/DSRLearn.Services.Settings/"]
RUN dotnet restore "./Systems/Worker/DSRLearn.Worker/DSRLearn.Worker.csproj"
COPY . .
WORKDIR "/src/Systems/Worker/DSRLearn.Worker"
RUN dotnet build "./DSRLearn.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DSRLearn.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DSRLearn.Worker.dll"]