# STILL Hotel Backend

The .NET 10 backend for a single-hotel booking system. Reservations exposes availability search against PostgreSQL and has controller methods reserved for sign-in, bookings, and staff workflows.

## Run locally

From the backend root, create any missing local configuration files in PowerShell.

~~~powershell
$api = 'services/Reservations/Reservations.Api'
if (!(Test-Path "$api/appsettings.json")) { Copy-Item "$api/appsettings.example.json" "$api/appsettings.json" }
if (!(Test-Path "$api/appsettings.Development.json")) { Copy-Item "$api/appsettings.Development.example.json" "$api/appsettings.Development.json" }
if (!(Test-Path '.env')) { Copy-Item '.env.example' '.env' }
~~~

Set `RESERVATIONS_DB_PASSWORD` in the ignored `.env` file to a password for your local database. If the database volume already exists, use its current password. With Docker running, start the API and seeded PostgreSQL database.

~~~sh
docker compose up --build
~~~

To run the API directly with the .NET 10 SDK, start PostgreSQL in Docker and run these commands in PowerShell.

~~~powershell
docker compose up -d --wait postgres
$env:ASPNETCORE_ENVIRONMENT = 'Development'
$env:ConnectionStrings__Reservations = 'Host=127.0.0.1;Port=5433;Database=reservations;Username=reservations'
$env:PGPASSWORD = Read-Host 'Enter your local PostgreSQL password from .env' -MaskInput
dotnet run --no-launch-profile --project services/Reservations/Reservations.Api -- --urls http://127.0.0.1:5080
~~~

Check [liveness](http://127.0.0.1:5080/health/live) or open the [generated OpenAPI document](http://127.0.0.1:5080/openapi/v1.json). See the [local runbook](docs/runbooks/local-stack.md) for troubleshooting.

Run `dotnet test HotelBooking.sln` to check the availability rules.

## Repository guide

- [Services](services/) groups each deployable with its own tests and container build. Notifications and Payments have milestone notes.
- [Infrastructure](infra/README.md) describes where shared local infrastructure will live.
- [Project rules](AGENTS.md) define how to structure services, controllers, and options. [Architecture](docs/architecture.md) describes the layers, environment settings, and implementation order.
- [API behavior](docs/api-behavior.md) describes the live and planned endpoints.
- [Decisions](docs/decisions/0001-reservations-own-inventory.md), [runbooks](docs/runbooks/), and [performance](docs/performance/README.md) explain design choices, recovery, and measurements.
- [Load tests](tests/load/README.md) outlines the first k6 scenarios.
- [HotelBooking.sln](HotelBooking.sln) opens the Reservations projects in Visual Studio.
