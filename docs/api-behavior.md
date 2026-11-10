# Reservations API behavior

## Available now

`GET /health/live` checks whether the API process is responding. In Development, `GET /openapi/v1.json` returns the generated contract.

`GET /api/availability` requires `checkIn`, `checkOut`, and `guests`. Send calendar dates as `yyyy-MM-dd` and the guest count as a positive integer. Check-in can start today in the hotel's configured time zone. Checkout must follow check-in and fall within the booking window, which defaults to 180 days from today. Invalid searches return `400` with field errors.

The response contains `checkIn`, `checkOut`, `guests`, `nights`, `currency`, and `rooms`. Each room type that fits the party and has inventory records for the entire stay includes `roomTypeId`, `name`, `maxGuests`, `isAvailable`, and `total`. The total sums nightly rates through the night before checkout. `isAvailable` is false if any of those nights is sold out. An empty `rooms` array means no room type fits the party and dates.

## Planned controller methods

These routes currently return `501 Not Implemented`. Their request and response contracts will be finalized when the corresponding use cases are added.

| Method | Route | Planned behavior |
| --- | --- | --- |
| GET | `/api/auth/sign-in` | Start guest or staff sign-in |
| POST | `/api/bookings` | Confirm a pay-at-hotel booking |
| GET | `/api/bookings` | List the signed-in guest's bookings |
| GET | `/api/bookings/{bookingId}` | View one of the signed-in guest's bookings |
| POST | `/api/bookings/{bookingId}/cancel` | Cancel one of the signed-in guest's bookings |
| GET | `/api/staff/arrivals` | List confirmed arrivals |
| GET | `/api/staff/rates` | List room rates |
| PUT | `/api/staff/rates/{roomTypeId}` | Update room rates |

At booking confirmation, Reservations rechecks the price and inventory. On a price or availability change, it returns `409 Conflict` so the guest can review the new price or select another room. Retrying a booking with the same `Idempotency-Key` and payload returns the original booking. Reusing the key with different details returns a conflict.

Guests may view and cancel their own bookings. Staff can see arrivals and change rates. Rate changes affect future quotes, while confirmed bookings retain their price. The cancellation cutoff is the start of check-in day in the hotel's time zone.
