# Reservations tests

Run `dotnet test HotelBooking.sln` from the backend root to check availability validation and hotel-local dates. Use PostgreSQL integration tests for inventory, transaction boundaries, and HTTP behavior.

Start with checkout-day availability and party-size filtering. When booking lands, verify that 50 requests with distinct idempotency keys competing for one room create exactly one booking. Also test retries after a lost response and repeated cancellation.
