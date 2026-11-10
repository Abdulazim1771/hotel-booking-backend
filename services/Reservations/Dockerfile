FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Reservations.Api/Reservations.Api.csproj Reservations.Api/
RUN dotnet restore Reservations.Api/Reservations.Api.csproj
COPY Reservations.Api/ Reservations.Api/
RUN dotnet publish Reservations.Api/Reservations.Api.csproj -c Release --no-restore -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
USER $APP_UID
ENTRYPOINT ["dotnet", "Reservations.Api.dll"]
