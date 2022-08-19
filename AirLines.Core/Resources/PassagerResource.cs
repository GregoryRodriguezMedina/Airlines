using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLines.Core.Resources
{
    public class PassagerResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
      
    }

    public class PassagerResponse: PassagerResource
    {
        public virtual ICollection<BookResponse> Books { get; set; }
    }
    public class PassagerRequest : PassagerResource { }
}
