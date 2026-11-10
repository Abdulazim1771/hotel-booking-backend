using Microsoft.AspNetCore.Mvc;

namespace Reservations.Api.Controllers;

[ApiController]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status501NotImplemented)]
public abstract class PlannedController : ControllerBase
{
    protected ObjectResult Pending() => Problem(
        statusCode: StatusCodes.Status501NotImplemented,
        detail: "This operation is planned for a later milestone.");
}
