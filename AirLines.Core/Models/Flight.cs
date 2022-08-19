
namespace AirLines.Core.Models
{
    public partial class Flight
    {
        public Flight()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int FromIdAirPort { get; set; }
        public int ToIdAirPort { get; set; }
        public decimal Price { get; set; }
        public decimal PriceChildren { get; set; }
        public int LimitAgeChildren { get; set; }
        public DateTime Date { get; set; }
        public int MinutesToArrive { get; set; }
        public DateTime DepartTime { get; set; }
        public DateTime BoardingTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime ArriveConfirmed { get; set; }
        public int Status { get; set; }

        public virtual AirPort FromIdAirPortNavigation { get; set; }
        public virtual AirPort ToIdAirPortNavigation { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}