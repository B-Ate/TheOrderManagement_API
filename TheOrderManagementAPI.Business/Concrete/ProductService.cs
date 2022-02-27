using System;
using System.Collections.Generic;
using TheOrderManagementAPI.Business.Abstract;
using TheOrderManagementAPI.Entities;

namespace TheOrderManagementAPI.Business.Concrete
{
    public class ProductService : IProductService
    {
        IProductDal _productDal;

        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void DeleteAllProductsInOrder(Guid OrderId)
        {
            List<Product> products = _productDal.GetList(q => q.OrderId == OrderId);

            foreach (Product product in products) 
               _productDal.Delete(product);
        }

        public void Delete(Guid ProductId)
        {
            Product product = _productDal.Get(q => q.Id == ProductId);
            _productDal.Delete(product);
        }

        public List<Product> GetAllProductsInOrder(Guid OrderId)
        {
            return _productDal.GetList(q => q.OrderId == OrderId);
        }

        public Product GetProduct(Guid ProductId)
        {
            return _productDal.Get(q => q.Id == ProductId);
        }


        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
