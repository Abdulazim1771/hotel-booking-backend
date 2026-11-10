using System.ComponentModel.DataAnnotations;

namespace Reservations.Application.Configuration;

public sealed class HotelOptions
{
    public const string SectionName = "Hotel";

    [Required]
    public string TimeZoneId { get; init; } = "";

    [Required, RegularExpression("^[A-Z]{3}$")]
    public string Currency { get; init; } = "";

    [Range(1, 366)]
    public int BookingWindowDays { get; init; }
}
