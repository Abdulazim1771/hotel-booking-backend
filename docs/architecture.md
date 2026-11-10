# Backend architecture

React calls the Reservations API over HTTP. In Development, Reservations serves its generated contract at `/openapi/v1.json`. See [API behavior](api-behavior.md) for what the frontend should expect.

| Service | Owns | Interface |
| --- | --- | --- |
| Reservations | Room types, rates, nightly inventory, bookings, and holds | HTTP and booking events |
| Notifications | Delivery records and processed event IDs | RabbitMQ booking events |
| Payments | Simulated payment attempts | Payment requests and outcomes |

Reservations stores inventory and bookings in PostgreSQL and confirms a stay in a single transaction. When notifications arrive, the same transaction also writes the event to an outbox. A relay publishes the event, and the worker handles repeated delivery using its own data. See the [inventory ownership decision](decisions/0001-reservations-own-inventory.md).

## Reservations layers

| Project | Responsibility | Project dependencies |
| --- | --- | --- |
| `Reservations.Domain` | Business concepts and invariants, such as valid stay dates | None |
| `Reservations.Application` | Use cases and the interfaces they need, such as availability inventory | Domain |
| `Reservations.Infrastructure` | PostgreSQL and other external implementations of those interfaces | Application |
| `Reservations.Api` | HTTP controllers, response mapping, and dependency registration | Application and Infrastructure |

Group code by feature within each project. Controllers bind HTTP requests and delegate to Application services. Keep PostgreSQL access in Infrastructure and wire it into Application through its inventory interface. See [the project rules](../AGENTS.md) for the conventions that apply to new code.

## Configuration

ASP.NET Core loads `appsettings.json` and then the overlay selected by `ASPNETCORE_ENVIRONMENT`. Copy the shared `appsettings.example.json` and the matching Development, Staging, or Production example to their runtime names by removing `.example`. Keep the resulting files outside Git. For deployment, mount them read-only or provide their settings through environment variables. Published builds and container images exclude the runtime files.

`HotelOptions` binds `Hotel` and `ReservationsDatabaseOptions` binds `ConnectionStrings`. Both validate on startup. Set `Hotel__TimeZoneId`, `Hotel__Currency`, and `Hotel__BookingWindowDays` to override the example hotel settings. Supply the deployed PostgreSQL connection string as `ConnectionStrings__Reservations` from your secret store. The `RabbitMq.ConnectionUri` placeholder is for the future notifications setup. Reservations currently reads PostgreSQL settings only.

Initialize each deployed database with [the inventory schema](../services/Reservations/Reservations.Infrastructure/Database/001-inventory.sql) and populate its room types and inventory. Local Compose mounts the ignored settings files and loads separate development fixtures.

## Implementation order

1. Availability search against seeded PostgreSQL inventory and generated OpenAPI is implemented.
2. Add sign-in, pay-at-hotel bookings, cancellation, and staff arrivals and rates.
3. Add RabbitMQ, the outbox relay, and the Notifications worker.
4. Exercise failure recovery, observability, and load across one and two API replicas.
5. Add simulated payments with expiring holds and late-approval handling.
