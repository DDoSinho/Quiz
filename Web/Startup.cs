using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dal;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dal.Model;
using Microsoft.EntityFrameworkCore;
using Dal.Model.Identity;

namespace Web
{
    public class Startup
    {
        public static long TimeStamp => new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();

        public Startup(IHostingEnvironment env)
        {

            using (var context = new QuizDbContext())
            {
                context.Database.EnsureCreated();
            }

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
            services.AddDbContext<LoginDbContext>(options => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=QuizDataBase;Trusted_Connection=True;"));

            services.AddIdentity<QuizUser, IdentityRole>().AddEntityFrameworkStores<LoginDbContext>();

            services.AddScoped<QuizDbContext>();
            services.AddScoped<QuestionManager>();

            services.AddMvc();
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
