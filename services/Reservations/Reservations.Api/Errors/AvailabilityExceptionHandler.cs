using Microsoft.AspNetCore.Diagnostics;
using Reservations.Application.Availability;

namespace Reservations.Api.Errors;

internal sealed class AvailabilityExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not InvalidAvailabilitySearchException validation)
            return false;

        await Results.ValidationProblem(new Dictionary<string, string[]>
        {
            [validation.Field] = [validation.Message]
        }).ExecuteAsync(context);
        return true;
    }
}
