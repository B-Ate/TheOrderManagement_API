using System;
using System.Collections.Generic;
using TheOrderManagementAPI.Entities;

namespace TheOrderManagementAPI.Business.Abstract
{
    public interface ICustomerService
    {
        Customer Get(Guid Id);
        Customer GetwithOrder(Guid OrderId);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Guid OrderId);
    }
}
