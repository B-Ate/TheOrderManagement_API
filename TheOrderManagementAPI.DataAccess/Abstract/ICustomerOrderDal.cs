﻿
using TheOrderManagementAPI.Core.DataAccess.EntityFramework;
using TheOrderManagementAPI.Entities;

namespace TheOrderManagementAPI.Business.Abstract
{
    public interface ICustomerOrderDal : IEntityRepository<CustomerOrder>
    {
    }
}
