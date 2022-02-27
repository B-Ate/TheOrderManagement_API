using System.Collections.Generic;

namespace TheOrderManagementAPI.Models
{
    public class CustomerOrderArgs
    {
        public CustomerDto Customer { get; set; }
        public List<ProductDto> Product { get; set; }
    }
}
