using Application;
using Application.Common.Interfaces;
using BugTracker.PageManagers;
using BugTracker.Persistance;
using BugTracker.Persistance.Abstract;
using Infrastructure;
using Infrastructure.Persistance;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebUI.Services;

namespace BugTrackerWeb
{
    //Verification
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddDbContext<ApplicationIdentityDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMemoryCache();

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAuditService, AuditService>();

            services.AddScoped<IRequestPersistance, RequestPersistance>();
            services.AddScoped<ITicketPersistance, TicketPersistance>();
            services.AddScoped<IProjectOwnerPersistance, ProjectOwnerPersistance>();
            services.AddScoped<IProjectPersistance, ProjectPersistance>();
            services.AddScoped<IQAPersistance, QAPersistance>();
            services.AddScoped<IQAManager, QAManager>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IDateTime, DateTimeService>();

            services.AddScoped<ITicketManager, TicketManager>();


            services.AddMvc();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.AddControllersWithViews();
            services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
