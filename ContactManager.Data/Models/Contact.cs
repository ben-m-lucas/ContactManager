using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManager.Data.Models
{
    public class Contact
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public Address PrimaryAddress { get; set; }
        public IList<PhoneNumber> PhoneNumbers { get; set; }
    }
}
