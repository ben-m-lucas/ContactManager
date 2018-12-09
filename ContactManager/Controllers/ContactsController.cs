using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Data;
using ContactManager.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase, IDisposable
    {
        private readonly ContactsContext _context;
        public ContactsController(ContactsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return Ok(contacts);
        }

        public async Task<IActionResult> CreateContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();
                return Ok(contact.Id);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}