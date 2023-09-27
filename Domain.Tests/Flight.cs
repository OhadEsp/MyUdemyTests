namespace Domain.Tests
{
    public class Flight
    {
        public int RemainingNumberOfSeats { get; set; }

        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }

        public void Book(string v1, int v2)
        {
            RemainingNumberOfSeats -= v2;
        }
    }
}