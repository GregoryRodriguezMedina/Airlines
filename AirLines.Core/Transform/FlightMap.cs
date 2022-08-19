using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLines.Core.Transform
{
    public static class FlightMap
    {
        public static List<Core.Resources.FlightResponse> TransfromObject(IEnumerable<Core.Models.Flight> models)
        {
            List<Core.Resources.FlightResponse> results = new List<Core.Resources.FlightResponse>();
            int len = models.Count();
            for (int i = 0; i < len; i++)
            {
                results.Add(TransfromObject(models.ElementAt(i)));
            }

            return results;
        }

        public static Core.Resources.FlightResponse TransfromObject(Core.Models.Flight model)
        {
            return new Core.Resources.FlightResponse
            {
                Date = model.Date,
                Id = model.Id,
                ArrivalTime = model.ArrivalTime,
                ArriveConfirmed = model.ArriveConfirmed,
                BoardingTime = model.BoardingTime,
                Code = model.Code,
                DepartTime = model.DepartTime,
                LimitAgeChildren = model.LimitAgeChildren,
                MinutesToArrive = model.MinutesToArrive,
                Price = model.Price,
                PriceChildren = model.PriceChildren,
                Status = model.Status,
                //FromIdAirPortNavigation
                //ToIdAirPortNavigation
            };
        }

        public static  Core.Models.Flight TransfromObject(Core.Resources.FlightRequest request)
        {
            return new Core.Models.Flight
            {
                Date = request.Date,
                Id = request.Id,
                ArrivalTime = request.ArrivalTime,
                ArriveConfirmed = request.ArriveConfirmed,
                BoardingTime = request.BoardingTime,
                Code = request.Code,
                DepartTime = request.DepartTime,
                LimitAgeChildren = request.LimitAgeChildren,
                MinutesToArrive = request.MinutesToArrive,
                Price = request.Price,
                PriceChildren = request.PriceChildren,
                Status = (int)this.DefaultStatus
            };
        }
    }
}
