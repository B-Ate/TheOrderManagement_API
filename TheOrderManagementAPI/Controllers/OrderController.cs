using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using TheOrderManagementAPI.Auth;
using TheOrderManagementAPI.Business.Abstract;
using TheOrderManagementAPI.Entities;
using TheOrderManagementAPI.Models;

namespace TheOrderManagementAPI.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ILogger<OrderController> _logger;
        IProductService _productService;
        ICustomerService _customerService;
        ICustomerOrderService _customerOrderService;

        public OrderController(ILogger<OrderController> logger, IProductService productService, ICustomerService customerService, ICustomerOrderService customerOrderService)
        {
            _logger = logger;
            _productService = productService;
            _customerService = customerService;
            _customerOrderService = customerOrderService;   
        }

        #region Customers

        [BasicAuthorization]
        [HttpGet]
        [Route("customers/{CustomerId}")]
        public Response GetCustomer(Guid CustomerId)
        {
            try
            {
                Customer customer = _customerService.Get(CustomerId);

                if (customer is null)
                    return SendResponse(false, "Not Found");

                CustomerDto customerDto = new CustomerDto();
                customerDto.Address = customer.Address;
                customerDto.Name = customer.Name;

                return SendResponse(true, "OK", customerDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return SendResponse(false, ex.Message);
            }
        }

        [BasicAuthorization]
        [HttpPut]
        [Route("customers/{CustomerId}")]
        public Response UpdateCustomer(Guid CustomerId, [FromBody] CustomerDto Customer)
        {
            try
            {
                Customer customer = _customerService.Get(CustomerId);

                if (customer is null)
                    return SendResponse(false, "Not Found");

                customer.Name = Customer.Name;
                customer.Address = Customer.Address;
                _customerService.Update(customer);

                return SendResponse(true, string.Format("OK Updated CustomerId : {0}", CustomerId));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return SendResponse(false, ex.Message);
            }
        }

        #endregion

        #region Products

        [BasicAuthorization]
        [HttpGet]
        [Route("products/{ProductId}")]
        public Response GetProduct(Guid ProductId)
        {
            try
            {
                Product product = _productService.GetProduct(ProductId);

                if (product is null)
                    return SendResponse(false, "Not Found");

                ProductDto productDto = new ProductDto();
                productDto.Barcode = product.Barcode;
                productDto.Description = product.Description;
                productDto.Quantity = product.Quantity;
                productDto.Price = product.Price;

                return SendResponse(true, "OK", productDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return SendResponse(false, ex.Message);
            }
        }

        [BasicAuthorization]
        [HttpGet]
        [Route("products/order/{OrderId}")]
        public Response GetProducts(Guid OrderId)
        {
            try
            {
                List<Product> product = _productService.GetAllProductsInOrder(OrderId);

                if (!product.Any())
                    return SendResponse(false, "Not Found");

                List<ProductDto> productDtos = product.Select(q => new ProductDto
                {
                    Barcode = q.Barcode,
                    Description = q.Description,
                    Quantity = q.Quantity,
                    Price = q.Price
                }).ToList();

                return SendResponse(true, "OK", productDtos);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return SendResponse(false, ex.Message);
            }
        }

        [BasicAuthorization]
        [HttpPut]
        [Route("product/{ProductId}")]
        public Response UpdateProduct(Guid ProductId, [FromBody] ProductDto productDto)
        {
            try
            {
                Product product = _productService.GetProduct(ProductId);

                if (product is null)
                    return SendResponse(false, "Not Found");

                product.Quantity = productDto.Quantity;
                product.Price = productDto.Price;
                product.Description = productDto.Description;
                product.Barcode = productDto.Barcode;
                _productService.Update(product);

                return SendResponse(true, string.Format("OK Updated ProductId : {0}", ProductId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return SendResponse(false, ex.Message);
            }
        }

        [BasicAuthorization]
        [HttpDelete]
        [Route("product/{ProductId}")]
        public Response DeleteProduct(Guid ProductId)
        {
            try
            {
                Product product = _productService.GetProduct(ProductId);

                if (product is null)
                    return SendResponse(false, "Not Found");

                _productService.Delete(ProductId);

                return SendResponse(true, string.Format("OK Deleted ProductId : {0}", ProductId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return SendResponse(false, ex.Message);
            }
        }

        [BasicAuthorization]
        [HttpPost]
        [Route("product")]
        public Response AddProduct([FromBody] ProductArgs product)
        {
            try
            {
                CustomerOrder customerOrder = _customerOrderService.GetCustomerOrder(product.OrderId);

                if (customerOrder is null)
                    return SendResponse(false, "CustomerOrder Not Found");

                Product newProduct = new Product
                {
                    Id = Guid.NewGuid(),
                    OrderId = product.OrderId,
                    Barcode = product.Barcode,
                    Price = product.Price,
                    Description = product.Description,
                    Quantity = product.Quantity
                };

                _productService.Add(newProduct);

                return SendResponse(true, "OK", string.Format("OK Created ProductId : {0}", newProduct.Id));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return SendResponse(false, ex.Message);
            }
        }

        #endregion

        #region CustomerOrder

        [BasicAuthorization]
        [HttpPost]
        [Route("order")]
        public Response AddCustomerOrder([FromBody] CustomerOrderArgs customerOrderDto)
        {
            try
            {
                CustomerOrder customerOrder = new CustomerOrder();
                Guid orderId = Guid.NewGuid();

                _customerOrderService.Add(new CustomerOrder { Id = orderId });

                Customer newCustomer = new Customer
                {
                     Id = Guid.NewGuid(),
                     OrderId = orderId,
                     Address = customerOrderDto.Customer.Address,
                     Name =  customerOrderDto.Customer.Name
                };

                _customerService.Add(newCustomer);

                if(customerOrderDto.Product.Any())
                {
                    List<Product> products = customerOrderDto.Product.Select(q => new Product
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        Barcode = q.Barcode,
                        Price = q.Price,
                        Description = q.Description,
                        Quantity = q.Quantity
                    }).ToList();

                    foreach (Product product in products)
                        _productService.Add(product);
                }
                    
                return SendResponse(true, string.Format("OK Created OrderId : {0}", orderId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return SendResponse(false, ex.Message);
            }
        }

        [BasicAuthorization]
        [HttpGet]
        [Route("order/{OrderId}")]
        public Response GetCustomerOrder(Guid OrderId)
        {
            try
            {
                CustomerOrderDto customerOrderDto = new CustomerOrderDto();

                CustomerOrder customerOrder = _customerOrderService.GetCustomerOrder(OrderId);

                if (customerOrder is null)
                    return SendResponse(false, "Not Found");

                Customer customer = _customerService.GetwithOrder(OrderId);
                customerOrderDto.Customer = new CustomerDto()
                {
                    Address = customer.Address,
                    Name = customer.Name
                };

                List<Product> products = _productService.GetAllProductsInOrder(OrderId);

                if (products.Any())
                {
                    customerOrderDto.Product = products.Select(q => new ProductDto
                    {
                        Barcode = q.Barcode,
                        Description = q.Description,
                        Quantity = q.Quantity,
                        Price = q.Price
                    }).ToList();

                    return SendResponse(true, "OK", customerOrderDto);
                }
                else
                    return SendResponse(false, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return SendResponse(false, ex.Message);
            }
        }

        [BasicAuthorization]
        [HttpDelete]
        [Route("customerOrder/{OrderId}")]
        public Response DeleteCustomerOrder(Guid OrderId)
        {
            try
            {
                CustomerOrder customerOrder = _customerOrderService.GetCustomerOrder(OrderId);

                if (customerOrder is null)
                    return SendResponse(false, "Not Found");

                List<Product> products = _productService.GetAllProductsInOrder(OrderId);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (products.Any())
                        _productService.DeleteAllProductsInOrder(OrderId);

                    _customerService.Delete(OrderId);
                    _customerOrderService.Delete(OrderId);

                    scope.Complete();
                }

                return SendResponse(true, String.Format("OK Created OrderId : {0}", OrderId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return SendResponse(false, ex.Message);
            }
        }

        #endregion

        #region GeneralMethods

        private Response SendResponse(bool IsSuccess, string Message, object Data = null)
        {
            return new Response
            {
                isSuccess = IsSuccess,
                message = Message,
                data = Data
            };
        }

        #endregion

    }
}
