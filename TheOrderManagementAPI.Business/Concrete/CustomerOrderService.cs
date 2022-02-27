using System;
using System.Collections.Generic;
using TheOrderManagementAPI.Business.Abstract;
using TheOrderManagementAPI.Entities;

namespace TheOrderManagementAPI.Business.Concrete
{
    public class CustomerOrderService : ICustomerOrderService
    {
        ICustomerOrderDal _customerOrderDal;

        public CustomerOrderService(ICustomerOrderDal customerOrderDal)
        {
            _customerOrderDal = customerOrderDal;
        }

        public void Add(CustomerOrder customerOrder)
        {
            _customerOrderDal.Add(customerOrder);
        }

        public void Delete(Guid OrderId)
        {
            CustomerOrder customerOrder = _customerOrderDal.Get(q => q.Id == OrderId);
            _customerOrderDal.Delete(customerOrder);
        }

        public CustomerOrder GetCustomerOrder(Guid OrderId)
        {
            return _customerOrderDal.Get(q => q.Id == OrderId);
        }
    }
}
