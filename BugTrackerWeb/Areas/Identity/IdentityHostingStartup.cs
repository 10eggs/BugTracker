using System;
using BugTrackerWeb.Areas.Identity.Data;
using BugTrackerWeb.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//[assembly: HostingStartup(typeof(BugTrackerWeb.Areas.Identity.IdentityHostingStartup))]
namespace BugTrackerWeb.Areas.Identity
{
    public class IdentityHostingStartup
    //: IHostingStartup
    {
        //    public void Configure(IWebHostBuilder builder)
        //    {
        //        builder.ConfigureServices((context, services) => {
        //            services.AddDbContext<AuthContext>(options =>
        //                options.UseSqlServer(
        //                    context.Configuration.GetConnectionString("AuthContextConnection")));

        //            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
        //                .AddEntityFrameworkStores<AuthContext>();
        //        });
        //    }
        //}
    }
}