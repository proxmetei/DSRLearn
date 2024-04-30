#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Api/DSRLearn.Api/DSRLearn.Api.csproj", "Systems/Api/DSRLearn.Api/"]
COPY ["Data/DSRLearn.Context.Seeder/DSRLearn.Context.Seeder.csproj", "Data/DSRLearn.Context.Seeder/"]
COPY ["Services/DSRLearn.Services.Debts/DSRLearn.Services.Debts.csproj", "Services/DSRLearn.Services.Debts/"]
COPY ["Data/DSRLearn.Context/DSRLearn.Context.csproj", "Data/DSRLearn.Context/"]
COPY ["Services/DSRLearn.Services.Settings/DSRLearn.Services.Settings.csproj", "Services/DSRLearn.Services.Settings/"]
COPY ["Shared/DSRLearn.Common/DSRLearn.Common.csproj", "Shared/DSRLearn.Common/"]
COPY ["Data/DSRLearn.Context.Entities/DSRLearn.Context.Entities.csproj", "Data/DSRLearn.Context.Entities/"]
COPY ["Services/DSRLearn.Services.UserAccount/DSRLearn.Services.UserAccount.csproj", "Services/DSRLearn.Services.UserAccount/"]
COPY ["Services/DSRLearn.Services.Actions/DSRLearn.Services.Actions.csproj", "Services/DSRLearn.Services.Actions/"]
COPY ["Services/DSRLearn.Services.RabbitMq/DSRLearn.Services.RabbitMq.csproj", "Services/DSRLearn.Services.RabbitMq/"]
COPY ["Data/Migrations/DSRLearn.Context.Migrations.MySql/DSRLearn.Context.Migrations.MySql.csproj", "Data/Migrations/DSRLearn.Context.Migrations.MySql/"]
COPY ["Data/Migrations/DSRLearn.Context.Migrations.PgSql/DSRLearn.Context.Migrations.PgSql.csproj", "Data/Migrations/DSRLearn.Context.Migrations.PgSql/"]
COPY ["Services/DSRLearn.Services.HostedMessage/DSRLearn.Services.HostedMessage.csproj", "Services/DSRLearn.Services.HostedMessage/"]
COPY ["Services/DSRLearn.Services.Logger/DSRLearn.Services.Logger.csproj", "Services/DSRLearn.Services.Logger/"]
COPY ["Services/DSRLearn.Services.Payments/DSRLearn.Services.Payments.csproj", "Services/DSRLearn.Services.Payments/"]
RUN dotnet restore "./Systems/Api/DSRLearn.Api/DSRLearn.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/DSRLearn.Api"
RUN dotnet build "./DSRLearn.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DSRLearn.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DSRLearn.Api.dll"]