# Planned API behavior

The first product endpoint is `GET /api/availability`. It takes `checkIn`, `checkOut`, and `guests`. Dates represent calendar days in the hotel's time zone. Checkout day is excluded from the nights charged or reserved. Results should identify room types and show the total for the requested stay.

At booking confirmation, Reservations rechecks the price and inventory. On a price or availability change, it returns `409 Conflict` so the guest can review the new price or select another room. Retrying a booking with the same `Idempotency-Key` and payload returns the original booking. Reusing the key with different details returns a conflict.

Guests may view and cancel their own bookings. Staff can see arrivals and change rates. Rate changes affect future quotes, while confirmed bookings retain their price. The cancellation cutoff is the start of check-in day in the hotel's time zone.

The generated OpenAPI document will describe HTTP inputs and responses. This guide captures behavior across requests. The scaffold currently exposes `GET /health/live`.
