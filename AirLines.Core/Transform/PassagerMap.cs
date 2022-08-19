using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLines.Core.Transform
{
    public static class PassagerMap
    {
        public static List<Core.Resources.PassagerResponse> TransfromObject(IEnumerable<Core.Models.Passager> models)
        {
            List<Core.Resources.PassagerResponse> results = new List<Core.Resources.PassagerResponse>();
            int len = models.Count();
            for (int i = 0; i < len; i++)
            {
                results.Add(TransfromObject(models.ElementAt(i)));
            }

            return results;
        }

        public static Core.Resources.PassagerResponse TransfromObject(Core.Models.Passager model)
        {
            return new Core.Resources.PassagerResponse
            {
                BirthDate = model.BirthDate,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = model.Id,
                //TODO: crear metodo para agregar los book
            };
        }

        public static Core.Models.Passager TransfromObject(Core.Resources.PassagerRequest request)
        {
            return new Core.Models.Passager
            {
                BirthDate = request.BirthDate,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Id = request.Id,
            };
        }
    }
}
