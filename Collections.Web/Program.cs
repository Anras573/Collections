using Collections.Domain.Contexts;
using Collections.Domain.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Collections.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationIdentityDbContext>();

                context.Database.Migrate();

                if (!context.Users.Any())
                {
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var config = host.Services.GetRequiredService<IConfiguration>();

                    var user = new User { UserName = config.GetSection("Admin")["Username"] };
                    var result = userManager.CreateAsync(user, config.GetSection("Admin")["Password"]).Result;

                    if (!result.Succeeded)
                    {
                        throw new ApplicationException(string.Join("/n", result.Errors.Select(x => x.Description)));
                    }
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
