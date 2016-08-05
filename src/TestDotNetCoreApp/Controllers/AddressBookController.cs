using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestDotNetCoreApp.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TestDotNetCoreApp.Controllers
{
    [Produces("application/json")]
    [Route("api/addressbook")]
   // [Authorize]
    public class AddressBookController : Controller
    {
        private readonly TestAppContext _context;

        public AddressBookController(TestAppContext context)
        {
            this._context = context;
        }

        // GET: api/values
        [Route("getAll"), HttpGet]
        public IEnumerable<AddressBook> Get()
        {
            var records = this._context.AddressBook.OrderBy(abr => abr.Subdivision);
            return records;
        }

        [Route("getrecord"), HttpGet]
        public IActionResult Get(int id)
        {
            return Json(this._context.AddressBook.FirstOrDefault(abr => abr.Id == id));
        }

        
    }
}
