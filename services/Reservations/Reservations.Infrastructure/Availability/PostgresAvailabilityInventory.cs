using Npgsql;
using Reservations.Application.Availability;
using Reservations.Domain.Availability;

namespace Reservations.Infrastructure.Availability;

public sealed class PostgresAvailabilityInventory(string connectionString) : IAvailabilityInventory, IDisposable
{
    private readonly NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);

    public async Task<IReadOnlyList<RoomAvailability>> FindAsync(
        StayDates stay, int guests, CancellationToken cancellationToken)
    {
        await using var command = dataSource.CreateCommand("""
            SELECT r.id, r.name, r.max_guests,
                   bool_and(n.rooms_reserved < n.rooms_total) AS is_available,
                   sum(n.nightly_rate) AS stay_total
            FROM room_types r
            JOIN nightly_inventory n ON n.room_type_id = r.id
            WHERE n.stay_date >= $1 AND n.stay_date < $2 AND r.max_guests >= $3
            GROUP BY r.id, r.name, r.max_guests
            HAVING count(*) = $4
            ORDER BY stay_total, r.id
            """);
        command.Parameters.Add(new NpgsqlParameter<DateOnly> { TypedValue = stay.CheckIn });
        command.Parameters.Add(new NpgsqlParameter<DateOnly> { TypedValue = stay.CheckOut });
        command.Parameters.Add(new NpgsqlParameter<int> { TypedValue = guests });
        command.Parameters.Add(new NpgsqlParameter<int> { TypedValue = stay.Nights });

        var rooms = new List<RoomAvailability>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            rooms.Add(new RoomAvailability(
                reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetBoolean(3), reader.GetDecimal(4)));
        }

        return rooms;
    }

    public void Dispose() => dataSource.Dispose();
}
