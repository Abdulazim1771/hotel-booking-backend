using System.ComponentModel.DataAnnotations;

namespace Reservations.Api.Configuration;

public sealed class ReservationsDatabaseOptions
{
    public const string SectionName = "ConnectionStrings";

    [Required(ErrorMessage = "Set ConnectionStrings__Reservations to the PostgreSQL connection string.")]
    public string Reservations { get; init; } = "";
}
