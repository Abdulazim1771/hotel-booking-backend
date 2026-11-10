# Backend project rules

Follow [the backend architecture](docs/architecture.md) and keep API behavior in sync with [the API guide](docs/api-behavior.md).

- Keep dependency arrows inward. Domain depends only on .NET. Application depends on Domain. Infrastructure implements Application interfaces. The API references Infrastructure at the composition root to wire implementations into dependency injection.
- Keep controllers thin. Bind HTTP inputs, pass cancellation tokens to Application services, and translate results to HTTP responses. Put business validation, pricing, authorization decisions about resource ownership, and transaction coordination in the use case or Domain.
- Group each layer by feature. Give each service one clear responsibility and define small interfaces for boundaries such as inventory access. Reuse business rules at their source. Add abstractions when a real boundary or repeated behavior calls for one.
- Bind each configuration section to a strongly typed options class at startup. Validate required settings with `ValidateOnStart` and inject typed values into consumers. Use `appsettings.json` for shared settings and environment files for overrides.
- Keep availability reads separate from writes. When booking is implemented, verify price and capacity and reserve all nights in one PostgreSQL transaction. Preserve idempotency and booking ownership in Application when exposing write endpoints.
- Keep planned controller actions at HTTP 501 until their use cases are ready. Update the API guide and verify HTTP behavior when enabling an action.

## Configuration and security

- Never stage or commit runtime `appsettings*.json`, local launch profiles, `.env` files, or user secrets. Keep their ignore rules in place and avoid force-adding these files. Track only explicitly named examples with public sample values or empty placeholders.
- Keep real connection strings, passwords, tokens, private keys, and customer data out of source, fixtures, documentation, logs, and build artifacts. Supply deployment secrets at runtime from the deployment's secret store. Use local credentials in ignored files or local environment variables.
- Exclude local configuration and credentials from Docker build contexts and published output. Load runtime settings through environment variables or read-only configuration mounts.
- Review staged changes before committing. Check that templates contain only sample values and that connection strings, authentication headers, and other secrets never appear in diagnostics.
