using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestDotNetCoreApp.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TestDotNetCoreApp.Controllers
{
    
    [Route("api/addressbook")]
    [Authorize]
    public class AddressBookController : Controller
    {
        private readonly TestAppContext _context;

        public AddressBookController(TestAppContext context)
        {
            this._context = context;
        }
        
        [Route("getAll"), HttpGet]
        [Produces("application/json")]
        public IEnumerable<AddressBook> Get()
        {
            var records = this._context.AddressBook.OrderBy(abr => abr.Subdivision);
            return records;
        }

        [Route("getRecord"), HttpGet]
        [Produces("application/xml")]
        public string Get(int id)
        {
            var serializer = new XmlSerializer(typeof(AddressBook));
            StringWriter writer = new StringWriter();
            var record = (this._context.AddressBook.FirstOrDefault(abr => abr.Id == id));
            serializer.Serialize(writer, record);
            return writer.ToString();
        }

        
    }
}
