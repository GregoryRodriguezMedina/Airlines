using System;
using System.Collections.Generic;

namespace AirLines.Core.Models
{
    public partial class AirPort
    {
        public AirPort()
        {
            FlightFromIdAirPortNavigations = new HashSet<Flight>();
            FlightToIdAirPortNavigations = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Flight> FlightFromIdAirPortNavigations { get; set; }
        public virtual ICollection<Flight> FlightToIdAirPortNavigations { get; set; }
    }
}