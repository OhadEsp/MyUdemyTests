using Xunit;
using System.Collections.Generic;
using System;
using FluentAssertions;

namespace Application.Tests
{
    public class FlightApplicationSpecification
    {
        [Fact]
        public void Books_flights()
        {
            var bookingService = new BookingService();

            // Store in DB.
            bookingService.Book(new BookDto(
                flightId: Guid.NewGuid(), passengerEmail: "a@b.com", numberOfSeats: 2 ));

            // Fetching data.
            bookingService.FindBooking().Should().ContainEquivalentOf(
                new BookingRm(passengerEmail: "a@b.com", numberOfSeats: 2)
                );
        }

        public class BookingService
        {
            public void Book(BookDto bookDto)
            {

            }

            public IEnumerable<BookingRm> FindBooking()
            {
                return new[]
                {
                    new BookingRm(passengerEmail: "a@b.com", numberOfSeats: 2)
                };
            }
        }

        // Dto = Data Transfer Object: sending information between a and b.
        public class BookDto
        {
            public BookDto(Guid flightId, string passengerEmail, int numberOfSeats)
            {
                   
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