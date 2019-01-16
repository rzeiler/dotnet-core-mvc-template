using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Urlaub.Data;

[assembly: HostingStartup(typeof(Urlaub.Areas.Identity.IdentityHostingStartup))]
namespace Urlaub.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDefaultIdentity<IdentityUser>(iu =>
                {
                    iu.Password.RequireDigit = false;
                    iu.Password.RequiredLength = 3;
                    iu.Password.RequireLowercase = false;
                    iu.Password.RequireUppercase = false;
                    iu.Password.RequireNonAlphanumeric = false;
                }).AddEntityFrameworkStores<ApplicationDbContext>();
            });
        }
    }
}