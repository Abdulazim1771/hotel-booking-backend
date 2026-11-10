# Payments

Add the payment simulator after pay-at-hotel and notifications are working. It will own payment attempts and support simulated approvals, declines, and delays. Reservations will own holds and booking status.

Keep the simulator, its integration tests, and its Dockerfile here. The first recovery tests should cover retries, expiry releasing the room, and late approval triggering a simulated void.
