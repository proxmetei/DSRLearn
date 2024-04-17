#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Identity/DSRLearn.Identity/DSRLearn.Identity.csproj", "Systems/Identity/DSRLearn.Identity/"]
COPY ["Data/DSRLearn.Context/DSRLearn.Context.csproj", "Data/DSRLearn.Context/"]
COPY ["Services/DSRLearn.Services.Settings/DSRLearn.Services.Settings.csproj", "Services/DSRLearn.Services.Settings/"]
COPY ["Shared/DSRLearn.Common/DSRLearn.Common.csproj", "Shared/DSRLearn.Common/"]
COPY ["Data/DSRLearn.Context.Entities/DSRLearn.Context.Entities.csproj", "Data/DSRLearn.Context.Entities/"]
RUN dotnet restore "./Systems/Identity/DSRLearn.Identity/DSRLearn.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/DSRLearn.Identity"
RUN dotnet build "./DSRLearn.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DSRLearn.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DSRLearn.Identity.dll"]