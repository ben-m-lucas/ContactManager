using ContactManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManager.Data
{
    public class ContactsContext : DbContext
    {
        public ContactsContext()
        {
        }

        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
    }
}
