using System;
using System.Collections.Generic;
using System.Text;

namespace HW6.Domain
{
    public class Book : Entity
    {
        public string TitleName { get; set; }
        public virtual ICollection<BooksAuthors> Authors { get; set; }
    }
}
