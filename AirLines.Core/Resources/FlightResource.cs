
namespace AirLines.Core.Resources
{
    public class FlightResource
    {
        public int Id { get; set; }
        public string Code { get; set; }
       // public int FromIdAirPort { get; set; }
       // public int ToIdAirPort { get; set; }
        public decimal Price { get; set; }
        public decimal PriceChildren { get; set; }
        public int LimitAgeChildren { get; set; }
        public DateTime Date { get; set; }
        public int MinutesToArrive { get; set; }
        public DateTime DepartTime { get; set; }
        public DateTime BoardingTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime ArriveConfirmed { get; set; }
       
    }

    public class FlightResponse : FlightResource
    {
        public int Status { get; set; }
        public virtual AirPortResorce FromIdAirPortNavigation { get; set; }
        public virtual AirPortResorce ToIdAirPortNavigation { get; set; }
        public virtual ICollection<BookResponse> Books { get; set; }
    }
    public class FlightRequest : FlightResource
    {
    }
}
