using Reservations.Application.Availability;
using Reservations.Application.Configuration;
using Reservations.Domain.Availability;

namespace Reservations.Tests;

public sealed class AvailabilityServiceTests
{
    private readonly RecordingInventory inventory = new();

    [Fact]
    public async Task SearchUsesHotelDateAndCountsOnlyNightsBeforeCheckout()
    {
        using var cancellation = new CancellationTokenSource();
        var result = await CreateService().SearchAsync(Day(2), Day(5), 2, cancellation.Token);

        Assert.Equal(3, result.Nights);
        Assert.Equal("USD", result.Currency);
        Assert.Equal(375m, Assert.Single(result.Rooms).Total);
        Assert.Equal(Day(2), inventory.Stay?.CheckIn);
        Assert.Equal(Day(5), inventory.Stay?.CheckOut);
        Assert.Equal(2, inventory.Guests);
        Assert.Equal(cancellation.Token, inventory.CancellationToken);
    }

    [Theory]
    [InlineData(2, 2, 2, "checkOut")]
    [InlineData(3, 2, 2, "checkOut")]
    [InlineData(2, 3, 0, "guests")]
    [InlineData(1, 3, 2, "checkIn")]
    [InlineData(2, 6, 2, "checkOut")]
    public async Task InvalidSearchDoesNotQueryInventory(int checkIn, int checkOut, int guests, string field)
    {
        var error = await Assert.ThrowsAsync<InvalidAvailabilitySearchException>(() =>
            CreateService().SearchAsync(Day(checkIn), Day(checkOut), guests, CancellationToken.None));

        Assert.Equal(field, error.Field);
        Assert.Null(inventory.Stay);
    }

    private AvailabilityService CreateService() => new(inventory, new HotelClock(), new HotelOptions
    {
        TimeZoneId = "Asia/Tashkent",
        Currency = "USD",
        BookingWindowDays = 3
    });

    private static DateOnly Day(int day) => new(2030, 1, day);

    private sealed class HotelClock : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => new(2030, 1, 1, 20, 0, 0, TimeSpan.Zero);
    }

    private sealed class RecordingInventory : IAvailabilityInventory
    {
        public StayDates? Stay { get; private set; }
        public int Guests { get; private set; }
        public CancellationToken CancellationToken { get; private set; }

        public Task<IReadOnlyList<RoomAvailability>> FindAsync(StayDates stay, int guests, CancellationToken cancellationToken)
        {
            Stay = stay;
            Guests = guests;
            CancellationToken = cancellationToken;
            return Task.FromResult<IReadOnlyList<RoomAvailability>>([new("standard", "Standard", 2, true, 375m)]);
        }
    }
}
