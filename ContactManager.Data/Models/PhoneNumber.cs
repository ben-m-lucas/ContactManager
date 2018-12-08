using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManager.Data.Models
{
    public class PhoneNumber
    {
        public long Id { get; set; }
        public PhoneType Type { get; set; }
        public string Number { get; set; }
    }
}
