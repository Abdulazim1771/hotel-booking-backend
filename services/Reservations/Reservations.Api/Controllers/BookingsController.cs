using Microsoft.AspNetCore.Mvc;

namespace Reservations.Api.Controllers;

[Route("api/bookings")]
public sealed class BookingsController : PlannedController
{
    [HttpPost]
    public IActionResult CreateBooking() => Pending();

    [HttpGet]
    public IActionResult ListMyBookings() => Pending();

    [HttpGet("{bookingId}")]
    public IActionResult GetBooking(string bookingId) => Pending();

    [HttpPost("{bookingId}/cancel")]
    public IActionResult CancelBooking(string bookingId) => Pending();
}
