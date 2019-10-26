using System;
using System.Collections.Generic;
using System.Text;

namespace HW6.Domain
{
    public class Client : Entity
    {
        public string FullName { get; set; }
        public bool IsDebtor { get; set; }
    }
}
