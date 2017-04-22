using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dal;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dal.Model;
using Microsoft.EntityFrameworkCore;
using Dal.Model.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Web.Services;

namespace Web
{
    public class Startup
    {
        public static long TimeStamp => new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
           .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QuizDbContext>(options =>  options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=QuizDataBase;Trusted_Connection=True;"));

            services.AddIdentity<QuizUser, IdentityRole>(options =>
            {
                options.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
            })
            .AddEntityFrameworkStores<QuizDbContext>();

            services.AddScoped<QuizDbContext>();
            services.AddScoped<QuestionManager>();
            services.AddTransient<IQuizService, QuizService>();

            services.AddMvc();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10000);
                options.CookieHttpOnly = true;
            });

            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
        }
    

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseIdentity();

            app.UseStaticFiles();

            app.UseSession();

            app.UseStatusCodePagesWithRedirects("/Home/Login");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}