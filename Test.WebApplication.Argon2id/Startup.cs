using Canducci.GeneratePassword.Argon2id.Extensions.DependencyInjection;
using Canducci.GeneratePassword.Argon2id;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.WebApplication.Argon2id.Models;
using MediatR;
namespace Test.WebApplication.Argon2id
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
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddDbContext<DatabaseContext>();
            services.AddArgon2idPasswordHasher(config =>
            {
                config.IterationCount = Argon2idConfiguration.DefaultIterationCount;
                config.MemorySizeKb = Argon2idConfiguration.DefaultMemorySizeKb;
                config.DegreeOfParallelism = Argon2idConfiguration.DefaultDegreeOfParallelism;
                config.SaltBytesLength = Argon2idConfiguration.DefaultSaltBytesLength;
                config.HashBytesLength = Argon2idConfiguration.DefaultHashBytesLength;
            });
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            services.AddControllersWithViews();
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

