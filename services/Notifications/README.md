# Notifications

Add the worker after booking and cancellation. It will consume booking events from RabbitMQ and keep delivery records in its own database. Use the event ID to recognize repeated deliveries.

Keep the worker, its integration tests, and its Dockerfile here. The first recovery test should stop the broker, create a booking, restore the broker, and verify that replaying the event leaves one delivery record.
