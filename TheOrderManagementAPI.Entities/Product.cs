using System;
using TheOrderManagementAPI.Core.Entities;

namespace TheOrderManagementAPI.Entities
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
