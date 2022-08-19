using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLines.Core.Types
{
    public enum Status
    {
        Bording = 0,
        Depart = 1,
        Arrive = 2,

    }

    public enum FlightInclude
    {
        Books = 0,
        FromAirPort = 1,
        ToAirPort = 2,
    }

    public enum BookInclude
    {
        Passager = 0,
        Flight = 1
    }
}
