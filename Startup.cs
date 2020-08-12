using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiddlewareWinAuth
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
            services.AddControllersWithViews();
            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
            services.AddAuthentication(HttpSysDefaults.AuthenticationScheme);
            // services.AddDataProtection()
            //     .PersistKeysToFileSystem(new DirectoryInfo(Configuration["certificateDirectory"]))
            //     .ProtectKeysWithCertificate(new X509Certificate2(Configuration["certificateName"], Configuration["certificatePassword"]));
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.Run(async (context) =>
            //{
            //    try
            //    {
            //        var user = (WindowsIdentity)context.User.Identity;

            //        await context.Response
            //                     .WriteAsync($"User: {user.Name}\tState: {user.ImpersonationLevel}\n");

            //        WindowsIdentity.RunImpersonated(user.AccessToken, () =>
            //        {
            //            var impersonatedUser = WindowsIdentity.GetCurrent();
            //            var message =
            //                $"User: {impersonatedUser.Name}\tState: {impersonatedUser.ImpersonationLevel}";

            //            var bytes = Encoding.UTF8.GetBytes(message);
            //            context.Response.Body.Write(bytes, 0, bytes.Length);
            //        });
            //    }
            //    catch (Exception e)
            //    {
            //        await context.Response.WriteAsync(e.ToString());
            //    }
            //});
        }
    }
}
