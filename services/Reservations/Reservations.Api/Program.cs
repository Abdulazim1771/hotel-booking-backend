using Microsoft.Extensions.Options;
using Reservations.Api.Configuration;
using Reservations.Api.Errors;
using Reservations.Application.Availability;
using Reservations.Application.Configuration;
using Reservations.Infrastructure.Availability;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<AvailabilityExceptionHandler>();

builder.Services.AddOptions<HotelOptions>()
    .BindConfiguration(HotelOptions.SectionName)
    .ValidateDataAnnotations()
    .Validate(options => TimeZoneInfo.TryFindSystemTimeZoneById(options.TimeZoneId, out _),
        "Hotel:TimeZoneId must identify a supported time zone.")
    .ValidateOnStart();
builder.Services.AddOptions<ReservationsDatabaseOptions>()
    .BindConfiguration(ReservationsDatabaseOptions.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton(services => services.GetRequiredService<IOptions<HotelOptions>>().Value);
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<IAvailabilityInventory>(services => new PostgresAvailabilityInventory(
    services.GetRequiredService<IOptions<ReservationsDatabaseOptions>>().Value.Reservations));
builder.Services.AddScoped<AvailabilityService>();

var app = builder.Build();
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.MapHealthChecks("/health/live");
app.MapControllers();
app.Run();
