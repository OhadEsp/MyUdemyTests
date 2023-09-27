using Xunit;
using FlightDomain;
using FluentAssertions;

namespace FlightTest
{
    public class FlightSpecifications
    {
        [Theory]
        [InlineData(3,1,2)]
        [InlineData(6,3,3)]
        [InlineData(10,6,4)]
        [InlineData(12,8,4)]
        public void Booking_reduces_the_number_of_seats(int seatCapacity, int numberOfSeats, int remainingNumberOfSeats)
        {
            // 3 Clear Purpose Steps

            // Given
            var flight = new Flight(seatCapacity: seatCapacity);

            // When
            flight.Book("ooo@tutorialeu.com", numberOfSeats);

            // Then
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        //[Fact]
        //public void Booking_reduces_the_number_of_seats()
        //{
        //    // 3 Clear Purpose Steps

        //    // Given
        //    var flight = new Flight(seatCapacity: 3);
            
        //    // When
        //    flight.Book("ooo@tutorialeu.com", 1);

        //    // Then
        //    flight.RemainingNumberOfSeats.Should().Be(2);
        //}

        //[Fact]
        //public void Booking_reduces_the_number_of_seats_2()
        //{
        //    // 3 Clear Purpose Steps

        //    // Given
        //    var flight = new Flight(seatCapacity: 6);

        //    // When
        //    flight.Book("ooo@tutorialeu.com", 3);

        //    // Then
        //    flight.RemainingNumberOfSeats.Should().Be(3);
        //}

        //[Fact]
        //public void Booking_reduces_the_number_of_seats_3()
        //{
        //    // 3 Clear Purpose Steps

        //    // Given
        //    var flight = new Flight(seatCapacity: 10);

        //    // When
        //    flight.Book("ooo@tutorialeu.com", 6);

        //    // Then
        //    flight.RemainingNumberOfSeats.Should().Be(4);
        //}

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

        [Fact]
        public void Books_flights_successfully()
        {
            var flight = new Flight(seatCapacity: 3);
            var error = flight.Book("ooo@tutorialeu.com", 1);
            error.Should().BeNull();
        }
    }
}