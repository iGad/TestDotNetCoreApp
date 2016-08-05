using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TestDotNetCoreApp.Configurations;
using TestDotNetCoreApp.Models;

namespace TestDotNetCoreApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(string.Format("appsettings.{0}.json", env.EnvironmentName), optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

            services.ReplaceDefaultViewEngine();

            // Register Entity Framework
            services.AddDbContext<TestAppContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                });
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 4,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                })
                .AddEntityFrameworkStores<TestAppContext>()
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseStaticFiles();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseNotDefaultIdentity();

            app.UseMvc();

            AddSampleUser(app.ApplicationServices).Wait();
            

        }

        private static async Task AddSampleUser(IServiceProvider provider)
        {
            using (var dbContext = provider.GetService<TestAppContext>())
            {
                var sqlServerDatabase = dbContext.Database;
                if (sqlServerDatabase != null)
                {
                    // Create database in user root (c:\users\your name)
                    if (await sqlServerDatabase.EnsureCreatedAsync())
                    {
                        var records = new List<AddressBook>
                        {
                            new AddressBook
                            {
                                FIO = "Петров Петр",
                                Email = "ghsdf@afs.com",
                                Phone = "58923458234",
                                Position = "Уборщик",
                                Subdivision = "Первое"
                            },
                            new AddressBook
                            {
                                FIO = "Иванов Иван",
                                Email = "ivan@ivan.ru",
                                Phone = "89013238432",
                                Position = "Менеджер",
                                Subdivision = "Первое"
                            },
                            new AddressBook
                            {
                                FIO = "Сидоров Семён",
                                Email = "sido@gmail.com",
                                Phone = "89084326754",
                                Position = "Слесарь",
                                Subdivision = "Второе"
                            }
                        };
                        
                        dbContext.AddressBook.AddRange(records);
                        
                        // add some users
                        var userManager = provider.GetService<UserManager<ApplicationUser>>();
                        
                        // add editor user
                        var testUser = new ApplicationUser
                        {
                            UserName = "test1"
                        };
                        var result = await userManager.CreateAsync(testUser, "test2");
                        dbContext.SaveChanges(true);
                    }

                }
            }

            //var userManager = provider.GetService<UserManager<ApplicationUser>>();
                
            //var testUser = new ApplicationUser
            //{
            //    UserName = "test1"
            //};
            //await userManager.CreateAsync(testUser, "test2");
                
        }
    }
}
