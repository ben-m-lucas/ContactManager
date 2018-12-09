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
    public class ContactsControllerTests : IDisposable
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

        public void Dispose()
        {
            ContactsContext.Contacts.RemoveRange(ContactsContext.Contacts.ToList());
            ContactsContext.SaveChanges();
        }

        public class when_retrieving_all_contacts : ContactsControllerTests
        {
            [Fact]
            public async Task it_returns_the_contacts_retrieved_from_the_data_layer()
            {
                // Arrange
                var contacts = new List<Contact>
                {
                    new Contact { Surname = "Doe", GivenName = "John" },
                    new Contact { Surname = "Doe", GivenName = "Jane" },
                    new Contact { Surname = "Surname", GivenName = "Given" }
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

        public class when_saving_a_new_contact : ContactsControllerTests
        {
            [Fact]
            public async Task it_saves_the_new_contact()
            {
                // Arrange 
                var contactToSave = new Contact
                {
                    GivenName = "John",
                    Surname = "Doe"
                };

                // Act
                var result = await Subject.CreateContact(contactToSave);

                //Assert
                result.Should().BeOfType<OkObjectResult>();
                ContactsContext.Contacts.Count().Should().Be(1);
                ContactsContext.Contacts.First().Should().BeEquivalentTo(contactToSave);
            }
            
            [Fact]
            public async Task it_returns_bad_request_if_model_state_is_not_valid()
            {
                // Arrange 
                var contactToSave = new Contact
                {
                    Surname = "Doe"
                };
                Subject.ModelState.AddModelError("GivenName", "Given Name is required");

                // Act
                var result = await Subject.CreateContact(contactToSave);

                //Assert
                result.Should().BeOfType<BadRequestObjectResult>();
                ContactsContext.Contacts.Count().Should().Be(0);
            }
        }
    }
}
