using System;
using System.Collections.Generic;
using System.Text;

namespace HW6.Domain
{
    public class BooksAuthors : Entity
    {
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
    }
}
