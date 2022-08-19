using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLines.Core.Resources
{
     public abstract class AirPortResorce
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AirPortResponse: AirPortResorce { }
    public class AirPortRequest: AirPortResorce { }
}
