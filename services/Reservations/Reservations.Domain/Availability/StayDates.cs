using System.Diagnostics.CodeAnalysis;

namespace Reservations.Domain.Availability;

public sealed record StayDates
{
    private StayDates(DateOnly checkIn, DateOnly checkOut)
    {
        CheckIn = checkIn;
        CheckOut = checkOut;
    }

    public DateOnly CheckIn { get; }
    public DateOnly CheckOut { get; }
    public int Nights => CheckOut.DayNumber - CheckIn.DayNumber;

    public static bool TryCreate(DateOnly checkIn, DateOnly checkOut, [NotNullWhen(true)] out StayDates? stay)
    {
        stay = checkOut > checkIn ? new StayDates(checkIn, checkOut) : null;
        return stay is not null;
    }
}
