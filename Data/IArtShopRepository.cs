using System.Collections.Generic;
using myArtShopCore.Data.Entities;

namespace myArtShopCore.Data
{
    public interface IArtShopRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string username,bool includeItems);
        Order GetOrderById(string username,int id);



        bool SaveAll();
        void AddEntity(object model);
        void AddOrder(Order newOrder);
    }
}