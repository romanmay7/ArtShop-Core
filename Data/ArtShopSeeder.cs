using Microsoft.AspNetCore.Hosting;
using myArtShopCore.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myArtShopCore.Data
{
    
    public class ArtShopSeeder
    {
        private readonly ArtShopContext _ctx;
        private readonly IHostingEnvironment _hosting;
        public ArtShopSeeder(ArtShopContext ctx,IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }   
        
        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if(!_ctx.Products.Any())
            {
                //Need to create sample data
                var filepath = Path.Combine(_hosting.ContentRootPath,"Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                _ctx.SaveChanges();


            }

            if (_ctx.Products.Any()&& (!_ctx.Orders.Any()))
            {
                //Need to create sample data

             _ctx.Orders.Add(new Order
             {
                 OrderDate = DateTime.Now,
                 OrderNumber = "ababab122",
                 Items = new List<OrderItem>()
               {
                   new OrderItem()
                   {
                       Product = _ctx.Products.First(), Quantity = 5, UnitPrice = _ctx.Products.First().Price
                   }
               }
             }); 

                _ctx.SaveChanges();


            }

            //var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
            //if (order == null)
            //{
            //    order.Items = new List<OrderItem>()
            //        {
            //       new OrderItem()
            //       {
            //           Product=products.First(),
            //           Quantity=5,
            //           UnitPrice = products.First().Price

            //       }
            //        };
            //}
        }
    }
}
