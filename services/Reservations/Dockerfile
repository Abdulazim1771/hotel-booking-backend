FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Reservations.Api/Reservations.Api.csproj Reservations.Api/
COPY Reservations.Application/Reservations.Application.csproj Reservations.Application/
COPY Reservations.Domain/Reservations.Domain.csproj Reservations.Domain/
COPY Reservations.Infrastructure/Reservations.Infrastructure.csproj Reservations.Infrastructure/
RUN dotnet restore Reservations.Api/Reservations.Api.csproj
COPY Reservations.Api/ Reservations.Api/
COPY Reservations.Application/ Reservations.Application/
COPY Reservations.Domain/ Reservations.Domain/
COPY Reservations.Infrastructure/ Reservations.Infrastructure/
RUN dotnet publish Reservations.Api/Reservations.Api.csproj -c Release --no-restore -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
USER $APP_UID
ENTRYPOINT ["dotnet", "Reservations.Api.dll"]
