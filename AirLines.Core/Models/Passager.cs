using System;
using System.Collections.Generic;

namespace AirLines.Core.Models
{
    public partial class Passager
    {
        public Passager()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}