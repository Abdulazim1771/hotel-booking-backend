using Reservations.Application.Configuration;
using Reservations.Domain.Availability;

namespace Reservations.Application.Availability;

public sealed class AvailabilityService(IAvailabilityInventory inventory, TimeProvider clock, HotelOptions hotel)
{
    private readonly TimeZoneInfo hotelTimeZone = TimeZoneInfo.FindSystemTimeZoneById(hotel.TimeZoneId);

    public async Task<AvailabilityResponse> SearchAsync(
        DateOnly checkIn, DateOnly checkOut, int guests, CancellationToken cancellationToken)
    {
        if (!StayDates.TryCreate(checkIn, checkOut, out var stay))
            throw new InvalidAvailabilitySearchException(nameof(checkOut), "Checkout must be after check-in.");

        if (guests < 1)
            throw new InvalidAvailabilitySearchException(nameof(guests), "Enter at least one guest.");

        var hotelNow = TimeZoneInfo.ConvertTime(clock.GetUtcNow(), hotelTimeZone);
        var today = DateOnly.FromDateTime(hotelNow.DateTime);
        if (checkIn < today)
            throw new InvalidAvailabilitySearchException(nameof(checkIn), "Check-in cannot be in the past.");

        if (checkOut.DayNumber - today.DayNumber > hotel.BookingWindowDays)
            throw new InvalidAvailabilitySearchException(nameof(checkOut), $"Checkout must be within {hotel.BookingWindowDays} days of today.");

        var rooms = await inventory.FindAsync(stay, guests, cancellationToken);
        return new AvailabilityResponse(checkIn, checkOut, guests, stay.Nights, hotel.Currency, rooms);
    }
}

public sealed record AvailabilityResponse(
    DateOnly CheckIn, DateOnly CheckOut, int Guests, int Nights, string Currency, IReadOnlyList<RoomAvailability> Rooms);
