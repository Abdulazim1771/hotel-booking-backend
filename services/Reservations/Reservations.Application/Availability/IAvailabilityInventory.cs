using Reservations.Domain.Availability;

namespace Reservations.Application.Availability;

public interface IAvailabilityInventory
{
    Task<IReadOnlyList<RoomAvailability>> FindAsync(StayDates stay, int guests, CancellationToken cancellationToken);
}

public sealed record RoomAvailability(string RoomTypeId, string Name, int MaxGuests, bool IsAvailable, decimal Total);
