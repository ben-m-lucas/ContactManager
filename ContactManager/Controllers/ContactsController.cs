using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var contacts = _context.Contacts.ToList();
            return Ok(contacts);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}