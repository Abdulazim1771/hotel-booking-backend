# Backend architecture

React calls the Reservations API over HTTP. The backend owns the API description and generates OpenAPI once the first product endpoint is available. See [API behavior](api-behavior.md) for what the frontend should expect.

| Service | Owns | Interface |
| --- | --- | --- |
| Reservations | Room types, rates, nightly inventory, bookings, and holds | HTTP and booking events |
| Notifications | Delivery records and processed event IDs | RabbitMQ booking events |
| Payments | Simulated payment attempts | Payment requests and outcomes |

Reservations stores inventory and bookings in PostgreSQL and confirms a stay in a single transaction. When notifications arrive, the same transaction also writes the event to an outbox. A relay publishes the event, and the worker handles repeated delivery using its own data. See the [inventory ownership decision](decisions/0001-reservations-own-inventory.md).

Inside Reservations, group HTTP handlers and use cases by Availability, Bookings, and Staff. Keep domain rules and persistence in separate folders within the API project as those concerns emerge. Each service gets its own tests and deployment artifact.

## Implementation order

1. Build availability search against seeded PostgreSQL inventory and expose OpenAPI.
2. Add sign-in, pay-at-hotel bookings, cancellation, and staff arrivals and rates.
3. Add RabbitMQ, the outbox relay, and the Notifications worker.
4. Exercise failure recovery, observability, and load across one and two API replicas.
5. Add simulated payments with expiring holds and late-approval handling.

The current scaffold runs Reservations and its liveness check.
