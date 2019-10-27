using System;
using System.Collections.Generic;
using System.Text;

namespace HW6.Domain
{
    public class Author : Entity
    {
        public string FullName { get; set; }
        public virtual ICollection<BooksAuthors> Books { get; set; }
    }
}
