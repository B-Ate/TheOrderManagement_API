using System;
using System.Collections.Generic;
using TheOrderManagementAPI.Entities;

namespace TheOrderManagementAPI.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAllProductsInOrder(Guid OrderId);
        Product GetProduct(Guid ProductId);
        void Add(Product product);
        void Update(Product product);
        void DeleteAllProductsInOrder(Guid OrderId);
        void Delete(Guid ProductId);
    }
}
