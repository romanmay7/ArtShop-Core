using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myArtShopCore.Data;
using myArtShopCore.Services;

namespace my_ArtShop_Core
{
    public class Startup

    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config =config;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ArtShopContext>(cfg=>
            {
                cfg.UseSqlServer(_config.GetConnectionString("ArtShopConnectionString"));
            });

            services.AddTransient<ArtShopSeeder>();
            services.AddScoped<IArtShopRepository, ArtShopRepository>();
            services.AddTransient<IMailService, NullMailService>();
            //Support for real mail service

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt=>opt.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/error");
            }
                //app.UseDefaultFiles(); //serving html via MVC
                app.UseStaticFiles();
                app.UseNodeModules(env);
                app.UseMvc(cfg=>
                {
                    cfg.MapRoute("Default", "/{controller}/{action}/{id?}", new { controller = "App", Action = "Index" });
                });
               
            

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
