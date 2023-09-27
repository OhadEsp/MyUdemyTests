using Xunit;
using FlightDomain;
using FluentAssertions;

namespace FlightTest
{
    public class FlightSpecifications
    {
        [Fact]
        public void Booking_reduces_the_number_of_seats()
        {
            // 3 Clear Purpose Steps

            // Given
            var flight = new Flight(seatCapacity: 3);
            
            // When
            flight.Book("ooo@tutorialeu.com", 1);

            // Then
            flight.RemainingNumberOfSeats.Should().Be(2);
        }

        [Fact]
        public void Avoids_overbooking()
        {
            // Given
            var flight = new Flight(seatCapacity: 3);

            // When
            var error = flight.Book("ooo@tutorialeu.com", 4);

            // Then
            error.Should().BeOfType<OverBookingError>();
        }
    }
}