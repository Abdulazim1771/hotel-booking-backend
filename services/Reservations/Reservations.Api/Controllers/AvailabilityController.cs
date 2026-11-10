using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Reservations.Application.Availability;

namespace Reservations.Api.Controllers;

[ApiController]
[Route("api/availability")]
public sealed class AvailabilityController(AvailabilityService availability) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<AvailabilityResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public Task<AvailabilityResponse> SearchAsync(
        [FromQuery, BindRequired] DateOnly checkIn,
        [FromQuery, BindRequired] DateOnly checkOut,
        [FromQuery, BindRequired] int guests,
        CancellationToken cancellationToken) =>
        availability.SearchAsync(checkIn, checkOut, guests, cancellationToken);
}
