using System;
using TheOrderManagementAPI.Core.Entities;

namespace TheOrderManagementAPI.Entities
{
    public class Customer : IEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
