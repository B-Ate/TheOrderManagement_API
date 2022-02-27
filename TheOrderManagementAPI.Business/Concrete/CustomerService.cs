using System;
using TheOrderManagementAPI.Business.Abstract;
using TheOrderManagementAPI.Entities;

namespace TheOrderManagementAPI.Business.Concrete
{
    public class CustomerService : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerService(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public void Add(Customer customer)
        {
            _customerDal.Add(customer);
        }

        public void Delete(Guid OrderId)
        {
            Customer customer = _customerDal.Get(q => q.OrderId == OrderId); 
            _customerDal.Delete(customer);
        }

        public Customer Get(Guid CustomerId)
        {
            return _customerDal.Get(q => q.Id == CustomerId);
        }

        public Customer GetwithOrder(Guid OrderId)
        {
            return _customerDal.Get(q => q.OrderId == OrderId);
        }

        public void Update(Customer customer)
        {
           _customerDal.Update(customer);
        }
    }
}
