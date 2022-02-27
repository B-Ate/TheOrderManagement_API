using TheOrderManagementAPI.Business.Abstract;
using TheOrderManagementAPI.Core.DataAccess.EntityFramework;
using TheOrderManagementAPI.DataAccess.Concrete.EntityFramework;
using TheOrderManagementAPI.Entities;

namespace TheOrderManagementAPI.Business.Concrete
{
    public class CustomerOrderDal : EntityFrameworkRepository<CustomerOrder, APIDbContext>, ICustomerOrderDal
    {
    }
}
