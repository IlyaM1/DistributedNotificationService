FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /source
COPY --link *.csproj .
RUN dotnet restore -r linux-musl-x64 --nologo
COPY --link . .
RUN dotnet publish -c Release -r linux-musl-x64 --nologo --self-contained true --no-restore -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine
EXPOSE 8080
WORKDIR /app
COPY --link --from=build /app .
USER $APP_UID
ENTRYPOINT ["./SmsNotificationService"]