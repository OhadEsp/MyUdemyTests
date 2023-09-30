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
            bookingService.Book(new BookDto());
            bookingService.FindBooking().Should().ContainEquivalentOf(new BookingRm());
        }

        public class BookingService
        {
            public void Book(BookDto bookDto)
            {

            }

            public IEnumerable<BookingRm> FindBooking()
            {
                throw new NotImplementedException();
            }
        }

        public class BookDto
        {

        }

        public class BookingRm
        {

        }
    }
}