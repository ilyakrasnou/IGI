using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreeRock.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FreeRock.Models;
using FreeRock.Hubs;

namespace FreeRock
{
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseRouting();
            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<CommentHub<Album>>("/CommentHub/" + typeof(Album).Name);
                routes.MapHub<CommentHub<Artist>>("/CommentHub/" + typeof(Artist).Name);
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/", new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "defaultPrivacy",
                    pattern: "/Privacy", new { controller = "Home", action = "Privacy" });

                endpoints.MapControllerRoute(
                    name: "ProjectsDetails",
                    pattern: "/Projects/Details/{id?}", new { controller = "Projects", action = "Details" });

                endpoints.MapControllerRoute(
                    name: "ProjectsIndex",
                    pattern: "/Projects", new { controller = "Projects", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "ProjectsEdit",
                    pattern: "/Projects/Edit/{id?}", new { controller = "Projects", action = "Edit" });

                endpoints.MapControllerRoute(
                    name: "RolesCreate",
                    pattern: "/Roles/Create/{id?}", new { controller = "Roles", action = "Create" });

                endpoints.MapControllerRoute(
                    name: "RolesEdit",
                    pattern: "/Roles/Edit/{id?}", new { controller = "Roles", action = "Edit" });


                endpoints.MapControllerRoute(
                    name: "UsersDetails",
                    pattern: "/Users/Details/{id?}", new { controller = "Users", action = "Details" });


                endpoints.MapControllerRoute(
                    name: "Users",
                    pattern: "/Users", new { controller = "Users", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "Users",
                    pattern: "/Users/Edit/{id?}", new { controller = "Users", action = "Edit" });

                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
            });*/
        }
    }
}
