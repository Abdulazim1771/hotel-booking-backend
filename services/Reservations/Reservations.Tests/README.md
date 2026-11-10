# Reservations tests

Add tests alongside the first feature. Use unit tests for date and cancellation rules and integration tests against PostgreSQL for inventory, transaction boundaries, and HTTP behavior.

Start with checkout-day availability and party-size filtering. When booking lands, verify that 50 requests with distinct idempotency keys competing for one room create exactly one booking. Also test retries after a lost response and repeated cancellation.
