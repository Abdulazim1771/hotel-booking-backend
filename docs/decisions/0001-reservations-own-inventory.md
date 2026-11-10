# 0001 Keep bookings and nightly inventory in Reservations

## Decision

Reservations owns nightly inventory and confirmed bookings in the same PostgreSQL database. A booking locks the relevant inventory rows in date order, verifies capacity and price, and reserves all nights in one transaction. Cancellation releases those nights in a single state transition.

## Reason

Availability can change between search and checkout. Saving the booking and updating inventory together makes competing requests resolve against the same source of truth. Splitting the writes across services would require a reservation protocol and recovery for partial failure. Simulated payments will introduce that exercise once an external approval is part of the workflow.

## Verification

Use real PostgreSQL to test 50 requests for one remaining room, adjacent stays, rollback on failed confirmation, and repeated cancellation.
