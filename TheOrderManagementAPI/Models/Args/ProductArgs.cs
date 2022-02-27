using System;

namespace TheOrderManagementAPI.Models
{
    public class ProductArgs
    {
        public Guid OrderId { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
