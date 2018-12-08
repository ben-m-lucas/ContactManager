using ContactManager.Controllers;
using ContactManager.Data;
using ContactManager.Data.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactManager.Tests.Controllers
{
    public class ContactsControllerTests
    {
        protected ContactsController Subject { get; private set; }
        protected ContactsContext ContactsContext { get; private set; }

        public ContactsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ContactsContext>()
                .UseInMemoryDatabase("test-database")
                .Options;
            ContactsContext = new ContactsContext(options);
            Subject = new ContactsController(ContactsContext);
        }

        public class when_retrieving_all_contacts : ContactsControllerTests
        {
            [Fact]
            public async Task it_returns_the_contacts_retrieved_from_the_data_layer()
            {
                // Arrange
                var contacts = new List<Contact>
                {
                    new Contact { Id = 1 },
                    new Contact { Id = 2 },
                    new Contact { Id = 3 }
                };
                await ContactsContext.Contacts.AddRangeAsync(contacts);
                await ContactsContext.SaveChangesAsync();

                // Act
                var result = await Subject.Get();

                // Assert
                result.Should().BeOfType<OkObjectResult>();
                result.As<OkObjectResult>().Value.Should().BeEquivalentTo(contacts);
            }
        }
    }
}
