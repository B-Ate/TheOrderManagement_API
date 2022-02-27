using System.Collections.Generic;

namespace TheOrderManagementAPI.Models
{
    public class CustomerOrderDto
    {
        public CustomerDto Customer { get; set; }
        public List<ProductDto> Product { get; set; }
    }
}
