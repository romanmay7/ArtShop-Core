using System.Collections.Generic;
using myArtShopCore.Data.Entities;

namespace myArtShopCore.Data
{
    public interface IArtShopRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);

        bool SaveAll();
        void AddEntity(object model);
    }
}