using ContactManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManager.Data
{
    public class ContactsContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
    }
}
