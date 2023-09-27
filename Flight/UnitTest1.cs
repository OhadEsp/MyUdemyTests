using Xunit;
using Domain.Tests;
using FluentAssertions;

namespace FlightTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var flight = new Flight(seatCapacity: 3);
            
            flight.Book("ooo@tutorialeu.com", 1);

            flight.RemainingNumberOfSeats.Should().Be(2);
        }
    }
}