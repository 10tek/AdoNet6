using System;
using System.Collections.Generic;
using System.Text;

namespace HW6.Domain
{
    public class Book : Entity
    {
        public string TitleName { get; set; }
        public Author Author { get; set; }
    }
}
