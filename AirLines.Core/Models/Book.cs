using System;
using System.Collections.Generic;

namespace AirLines.Core.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int PassagerId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public virtual Flight Flight { get; set; }
        public virtual Passager Passager { get; set; }
    }
}