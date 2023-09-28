namespace FlightDomain
{
    public class Flight
    {
        List<Booking> bookingList = new();
        public IEnumerable<Booking> BookingList => bookingList;
        //public List<Booking> BookingList { get; set; } = new List<Booking>();
        public int RemainingNumberOfSeats { get; set; }

        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }

        public object? Book(string passengerEmail, int numberOfSeats)
        {
            if (numberOfSeats > RemainingNumberOfSeats)
                return new OverBookingError();

            RemainingNumberOfSeats -= numberOfSeats;

            bookingList.Add(new Booking(passengerEmail, numberOfSeats));
            return null;
        }
    }
}