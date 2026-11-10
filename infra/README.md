# Local infrastructure

`compose.yaml` at the backend root defines the local stack. Add PostgreSQL with availability search, Keycloak with guest and staff sign-in, RabbitMQ with notifications, and telemetry configuration when tracing spans multiple services.

Keep service-specific migrations and container builds with their service. Put shared configuration, such as a development Keycloak realm export or an OpenTelemetry collector configuration, here as each dependency is introduced.
