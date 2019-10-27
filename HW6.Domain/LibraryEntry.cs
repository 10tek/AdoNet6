using System;
using System.Collections.Generic;
using System.Text;

namespace HW6.Domain
{
    public class LibraryEntry : Entity
    {
        public virtual Client Client { get; set; }
        public virtual Book Book { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
