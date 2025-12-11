# Базовый образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY NotificationGateway.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app


ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true

RUN adduser --disabled-password --home /app --gecos '' dotnetuser \
    && chown -R dotnetuser:dotnetuser /app
USER dotnetuser

COPY --from=build --chown=dotnetuser:dotnetuser /app/publish .


EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "NotificationGateway.dll"]