namespace Reservations.Application.Availability;

public sealed class InvalidAvailabilitySearchException(string field, string message) : Exception(message)
{
    public string Field { get; } = field;
}
