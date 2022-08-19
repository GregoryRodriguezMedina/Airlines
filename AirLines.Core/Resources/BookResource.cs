namespace AirLines.Core.Resources
{
    public class BookResource
    {
        public int Id { get; set; }
        //public int FlightId { get; set; }
       // public int PassagerId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
     
    }
    public class BookResponse: BookResource
    {
        public FlightResponse Flight { get; set; }
        public PassagerResponse Passager { get; set; }
    }
    public class BookRequest: BookResource
    {
    }
}
