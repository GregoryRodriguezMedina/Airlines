using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLines.Core.Transform
{
    public static class BookMap
    {
        public static List<Core.Resources.BookResponse> TransfromObject(IEnumerable<Core.Models.Book> models)
        {
            List<Core.Resources.BookResponse> results = new List<Core.Resources.BookResponse>();
            int len = models.Count();
            for (int i = 0; i < len; i++)
            {
                results.Add(TransfromObject(models.ElementAt(i)));
            }

            return results;
        }

        public static Core.Resources.BookResponse TransfromObject(Core.Models.Book model)
        {
            return new Core.Resources.BookResponse
            {
                CheckIn = model.CheckIn,
                CheckOut = model.CheckOut,
                Id = model.Id,
                Date = model.Date,
                Price = model.Price,
                //Flight
                //Passager

            };
        }

        public static  Core.Models.Book TransfromObject(Core.Resources.BookRequest request)
        {
            return new Core.Models.Book
            {
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                Id = request.Id,
                Date = request.Date,
                Price = request.Price,
            };
        }
    }
}
