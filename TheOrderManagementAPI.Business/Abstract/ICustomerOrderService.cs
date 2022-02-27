using System;
using System.Collections.Generic;
using TheOrderManagementAPI.Entities;

namespace TheOrderManagementAPI.Business.Abstract
{
    public interface ICustomerOrderService
    {
        CustomerOrder GetCustomerOrder(Guid OrderId);
        void Add(CustomerOrder customerOrder);
        void Delete(Guid OrderId);
    }
}
