namespace FlightDomain
{
    public class Flight
    {
        public int RemainingNumberOfSeats { get; set; }

        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }

        public object? Book(string passengerEmail, int numberOfSeats)
        {
            if (numberOfSeats > RemainingNumberOfSeats)
            {
                return new OverBookingError();
            }

            RemainingNumberOfSeats -= numberOfSeats;
            return null;
        }
    }
}