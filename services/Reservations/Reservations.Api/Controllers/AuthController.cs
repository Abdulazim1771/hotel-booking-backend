using Microsoft.AspNetCore.Mvc;

namespace Reservations.Api.Controllers;

[Route("api/auth")]
public sealed class AuthController : PlannedController
{
    [HttpGet("sign-in")]
    public IActionResult SignIn() => Pending();
}
