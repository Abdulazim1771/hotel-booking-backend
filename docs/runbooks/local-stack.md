# Local stack runbook

## Check startup

1. Create the ignored local settings files and set your database password in `.env` as described in the [README](../../README.md#run-locally). From the backend root, run `docker compose config --quiet` to validate the Compose file.
2. Start Docker and run `docker compose up --build`. PostgreSQL creates the schema and seeds the next 180 nights when the data volume is initialized.
3. Open `http://127.0.0.1:5080/health/live`. A `200` response means the Reservations process is answering.
4. If the response fails, check `docker compose ps` and `docker compose logs reservations`. An address-in-use error usually means another process owns port 5080. Stop the other process if it is yours, or change the published host port in Compose.
5. Stop the local stack with `docker compose down`.

See the [README](../../README.md#run-locally) for running directly with the .NET SDK.

If the local seed dates have aged out, run `docker compose exec postgres psql -U reservations -d reservations -v ON_ERROR_STOP=1 -f /docker-entrypoint-initdb.d/002-development-inventory.sql`. This adds missing dates while preserving existing rows.
