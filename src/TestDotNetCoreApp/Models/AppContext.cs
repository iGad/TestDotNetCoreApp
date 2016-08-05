using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestDotNetCoreApp.Models
{
    public class ApplicationUser : IdentityUser { }
    public class TestAppContext : IdentityDbContext<ApplicationUser>
    {
        public TestAppContext(DbContextOptions<TestAppContext> options) : base(options)
        { }

        public DbSet<AddressBook> AddressBook { get; set; }
    }
}
