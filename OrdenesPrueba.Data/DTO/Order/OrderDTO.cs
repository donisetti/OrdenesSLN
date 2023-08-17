using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosPOC.Data.DTO.Order
{
   public class OrderDTO
    {
        public OrderDTO()
        {
            ItemOrderItemDto = new List<ItemOrderItemDto>();
        }
        public int OrderId { get; set; }
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Statu { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? LastModificated { get; set; }
        public DateTime? DateDelete { get; set; }
        public bool Process { get; set; }
        public bool Success { get; set; }
        public string Mesage { get; set; }
        public List<ItemOrderItemDto> ItemOrderItemDto { get; set; }
    }


    public class ListOrderDto
    {
        public ListOrderDto()
        {
            ItemOrder = new List<ItemOrderDto>();
        }
        public List<ItemOrderDto> ItemOrder { get; set; }
    }
    public class ItemOrderDto
    {
        public ItemOrderDto(Models.Order Order)
        {
            OrderId = Order.OrderId;
            CustomerId = Order.CustomerId;
            OrderNumber = Order.OrderNumber;
            OrderDate = Order.OrderDate;
            TotalAmount = Order.TotalAmount;
            Statu = Order.Statu;
        }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "Status")]
        public bool Statu { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? LastModificated { get; set; }
        public DateTime? DateDelete { get; set; }
    }

    public class ListCustomerDto
    {
        public ListCustomerDto()
        {
            ItemCustomer = new List<ItemCustomerDto>();
        }
        public List<ItemCustomerDto> ItemCustomer { get; set; }
    }
    public class ItemCustomerDto
    {
        public ItemCustomerDto(Models.Customer customer)
        {
            CustomerId = customer.CustomerId;
            CustomerName = customer.CustomerName;
        }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }

    public class ListProductDto
    {
        public ListProductDto()
        {
            ItemProduct = new List<ItemProductDto>();
        }
        public List<ItemProductDto> ItemProduct { get; set; }
    }
    public class ItemProductDto
    {
        public ItemProductDto(Models.Product product)
        {
            ProductId = product.ProductId;
            ProductName = product.ProductName;
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }

    public class ListItemOrderDto
    {
        public ListItemOrderDto()
        {
            ItemOrderItemDto = new List<ItemOrderItemDto>();
        }
        public List<ItemOrderItemDto> ItemOrderItemDto { get; set; }
    }
    public class ItemOrderItemDto
    {
        public ItemOrderItemDto()
        {

        }
        public ItemOrderItemDto(Models.OrderItem orderItem,string Product, int Posicion)
        {
            UnitPrice = orderItem.UnitPrice;
            Quantity = orderItem.Quantity;
            OrderId = orderItem.OrderId;
            ProductId = orderItem.ProductId;
            this.Product = Product;
            this.Posicion = Posicion;
        }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public bool Statu { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public int Posicion { get; set; }
        [Display(Name = "Total")]
        public decimal? Total { get; set; }
    }

    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool IsDiscontinued { get; set; }
        public bool Statu { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? LastModificated { get; set; }
        public DateTime? DateDelete { get; set; }
    }
}
