using Xunit;
using System.Collections.Generic;
using System;
using FluentAssertions;
using Data;
using FlightDomain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Application.Tests;

namespace Application.Tests
{
    public class FlightApplicationSpecification
    {
        // entities is like our database.
        readonly Entities entities = new Entities(new DbContextOptionsBuilder()
                    .UseInMemoryDatabase("Flights")
                    .Options);
        readonly BookingService bookingService;

        public FlightApplicationSpecification()
        {
            // This service can book flights for us and it can find flights.
            bookingService = new BookingService(entities: entities);
        }

        [Theory]
        [InlineData("m@m.com", 2)]
        [InlineData("a@a.com", 2)]
        public void Books_flights(string passengerEmail, int numberOfSeats)
        {
            // We create a new flight and add it to our database.
            var flight = new Flight(3);
            entities.Flights.Add(flight);

            // Store in DB.
            bookingService.Book(new BookDto(
                flightId: flight.Id, passengerEmail, numberOfSeats));

            // Fetching data.
            bookingService.FindBooking(flight.Id).Should().ContainEquivalentOf(
                new BookingRm(passengerEmail, numberOfSeats)
                );
        }

        [Fact]
        public void Cancels_booking()
        {
            // Given
            var flight = new Flight(3);
            entities.Flights.Add(flight);

            bookingService.Book(new BookDto(flightId: flight.Id,
                passengerEmail: "m@m.com",
                numberOfSeats: 2));

            // When
            bookingService.CancelBooking(
                new CancelBookingDto(flightId: flight.Id,
                    passengerEmail: "m@m.com",
                    numberOfSeats: 2)
                    );

            // Then
            bookingService.GetRemainingNumberOfSeatsFor(flight.Id).Should().Be(3);
        }
    }
}