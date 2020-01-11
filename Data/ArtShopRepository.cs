using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using myArtShopCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myArtShopCore.Data
{
    public class ArtShopRepository : IArtShopRepository
    {
        private readonly ArtShopContext _context;
        private readonly ILogger<ArtShopRepository> _logger;


        public ArtShopRepository(ArtShopContext context,ILogger<ArtShopRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts");
                return _context.Products.OrderBy(prop => prop.Title).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products.Where(p => p.Category == category).ToList();
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            try
            {
                if(includeItems)
                {
                    _logger.LogInformation("GetAllOrders");

                    return _context.Orders
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();
                }
                else
                {
                    return _context.Orders.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username,bool includeItems)
        {
            try
            {
                if (includeItems)
                {
                    _logger.LogInformation("GetAllOrders");

                    return _context.Orders
                        .Where(o=>o.User.UserName==username)
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();
                }
                else
                {
                    return _context.Orders.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }
        public Order GetOrderById(string username,int id)
        {
            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id && o.User.UserName==username)
                .FirstOrDefault();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public void AddOrder(Order newOrder)
        {
           //Convert new product to lookup of product
           foreach(var item in newOrder.Items)
            {
                item.Product = _context.Products.Find(item.Product.Id);

            }

            AddEntity(newOrder);
        }
    }
}
