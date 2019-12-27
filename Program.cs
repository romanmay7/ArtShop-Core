using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using myArtShopCore.Data;

namespace my_ArtShop_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host=BuildWebHost(args);

            SeedDb(host);

            host.Run();
        }

        private static void SeedDb(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using(var scope=scopeFactory.CreateScope())
           {
                var seeder =scope.ServiceProvider.GetService<ArtShopSeeder>();
                seeder.SeedAsync().Wait();
            }
        }

        public static IWebHost BuildWebHost(string[] args)=>
        
             WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(SetupConfiguration)
            .UseStartup<Startup>()
            .Build();
        

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            //removing the default configuration options
            builder.Sources.Clear();
            builder.AddJsonFile("config.json", false, true)
                //.AddXmlFile("config.xml,true")
                .AddEnvironmentVariables();
                
        }
    }
}
