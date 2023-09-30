using Xunit;
using System.Collections.Generic;
using System;
using FluentAssertions;
using Data;
using FlightDomain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Tests
{
    public class FlightApplicationSpecification
    {
        [Theory]
        [InlineData("m@m.com", 2)]
        [InlineData("a@a.com", 2)]
        public void Books_flights(string passengerEmail, int numberOfSeats)
        {
            // entities is like our database.
            var entities = new Entities(new DbContextOptionsBuilder()
                .UseInMemoryDatabase("Flights")
                .Options);

            // We create a new flight and add it to our database.
            var flight = new Flight(3);
            entities.Flights.Add(flight);

            // This service can book flights for us and it can find flights.
            var bookingService = new BookingService(entities: entities);

            // Store in DB.
            bookingService.Book(new BookDto(
                flightId: flight.Id, passengerEmail, numberOfSeats));

            // Fetching data.
            bookingService.FindBooking(flight.Id).Should().ContainEquivalentOf(
                new BookingRm(passengerEmail, numberOfSeats)
                );
        }

        public class BookingService
        {
            public Entities Entities { get; set; }
            public BookingService(Entities entities)
            {
                Entities = entities;
            }
            public void Book(BookDto bookDto)
            {
                var flight = Entities.Flights.Find(bookDto.FlightId);
                flight.Book(bookDto.PassengerEmail, bookDto.NumberOfSeats);
                Entities.SaveChanges();
            }

            public IEnumerable<BookingRm> FindBooking(Guid flightId)
            {
                return Entities.Flights
                    .Find(flightId)
                    .BookingList
                    .Select(booking => new BookingRm(
                        booking.Email,
                        booking.NumberOfSeats
                        ));
            }
        }

        // Dto = Data Transfer Object: sending information between a and b.
        public class BookDto
        {
            public Guid FlightId { get; set; }
            public string PassengerEmail {  get; set; }
            public int NumberOfSeats { get; set; }

            public BookDto(Guid flightId, string passengerEmail, int numberOfSeats)
            {
                FlightId = flightId;
                PassengerEmail = passengerEmail;
                NumberOfSeats = numberOfSeats;
            }
        }

        // Rm = ReadModel: Ised for providing specific information.
        public class BookingRm
        {
            public string PassengerEmail { get; set; }
            public int NumberOfSeats { get; set; }
            public BookingRm(string passengerEmail, int numberOfSeats)
            {
                PassengerEmail = passengerEmail;
                NumberOfSeats = numberOfSeats;
            }
        }
    }
}