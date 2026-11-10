using Microsoft.AspNetCore.Mvc;

namespace Reservations.Api.Controllers;

[Route("api/staff")]
public sealed class StaffController : PlannedController
{
    [HttpGet("arrivals")]
    public IActionResult ListArrivals() => Pending();

    [HttpGet("rates")]
    public IActionResult ListRates() => Pending();

    [HttpPut("rates/{roomTypeId}")]
    public IActionResult UpdateRate(string roomTypeId) => Pending();
}
