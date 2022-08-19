using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLines.Core.Transform
{
    public static class AirPortMap
    {
        public static List<Core.Resources.AirPortResponse> TransfromObject(IEnumerable<Core.Models.AirPort> models)
        {
            List<Core.Resources.AirPortResponse> results = new List<Core.Resources.AirPortResponse>();
            int len = models.Count();
            for (int i = 0; i < len; i++)
            {
                results.Add(TransfromObject(models.ElementAt(i)));
            }

            return results;
        }

        public static Core.Resources.AirPortResponse TransfromObject(Core.Models.AirPort model)
        {
            return new Core.Resources.AirPortResponse
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static Core.Models.AirPort TransfromObject(Core.Resources.AirPortRequest request)
        {
            return new Core.Models.AirPort
            {
                Id = request.Id,
                Name = request.Name
            };
        }
    }
}
