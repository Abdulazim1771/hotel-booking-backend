# STILL Hotel Backend

The .NET backend for a single-hotel booking system. The Reservations API currently exposes a liveness check. The first product endpoint will be availability search.

## Run locally

With the .NET 10 SDK installed, run this from the backend root:

~~~sh
dotnet run --project services/Reservations/Reservations.Api -- --urls http://127.0.0.1:5080
~~~

With Docker running, use this command instead:

~~~sh
docker compose up --build
~~~

Open [the liveness endpoint](http://127.0.0.1:5080/health/live) to check that the API is responding. See the [local runbook](docs/runbooks/local-stack.md) for troubleshooting.

## Repository guide

- [Services](services/) groups each deployable with its own tests and container build. Notifications and Payments have milestone notes.
- [Infrastructure](infra/README.md) describes where shared local infrastructure will live.
- [Architecture](docs/architecture.md) describes service boundaries and the implementation order.
- [API behavior](docs/api-behavior.md) records the planned frontend integration.
- [Decisions](docs/decisions/0001-reservations-own-inventory.md), [runbooks](docs/runbooks/), and [performance](docs/performance/README.md) explain design choices, recovery, and measurements.
- [Load tests](tests/load/README.md) outlines the first k6 scenarios.
- [Sample projects](microservices/) contain the earlier Writer, Article, and gateway exercise. The hotel solution is `HotelBooking.sln`.
