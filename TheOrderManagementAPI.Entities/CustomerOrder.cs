using System;
using TheOrderManagementAPI.Core.Entities;

namespace TheOrderManagementAPI.Entities
{
    public class CustomerOrder : IEntity
    {
        public Guid Id { get; set; }
       // public DateTime CreatedDate { get; set; }
        //public DateTime ModifiedDate { get; set; }
    }
}
