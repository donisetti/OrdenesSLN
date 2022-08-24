using Microsoft.EntityFrameworkCore;
using OrdenesPrueba.Data.DTO.Order;
using OrdenesPrueba.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenesPrueba.Data.Repository.Implemetation
{
    public class OrderRepository : IOrderRepository
    {
        public OrderCustomerBDContext _context;
        public OrderRepository(OrderCustomerBDContext _context)
        {
            this._context = _context;
        }

        public List<ItemCustomerDto> getAllCustomer()
        {
            List<ItemCustomerDto> ListCustomer = new List<ItemCustomerDto>();
            try
            {
                ListCustomer = _context.Customers.Where(x => x.Statu).Select(x => new ItemCustomerDto(x)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListCustomer;
        }

        public async Task<ListOrderDto> getAllOrder()
        {
            ListOrderDto ListOrder = new ListOrderDto();
            try
            {
                ListOrder.ItemOrder = await _context.Orders.Where(x => x.Statu).Select(x => new ItemOrderDto(x)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListOrder;
        }

        public List<ItemProductDto> getAllProduct()
        {
            List<ItemProductDto> ListProduct = new List<ItemProductDto>();
            try
            {
                ListProduct = _context.Products.Where(x => x.Statu && !x.IsDiscontinued).Select(x => new ItemProductDto(x)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListProduct;
        }

        public ProductDTO getProductById(int IdProduct)
        {
            ProductDTO Product = new ProductDTO();
            try
            {
               Models.Product ProductDb = _context.Products.FirstOrDefault(x =>  x.ProductId == IdProduct);
                if (ProductDb != null)
                {
                    Product.ProductId = ProductDb.ProductId;
                    Product.IsDiscontinued = ProductDb.IsDiscontinued;
                    Product.SupplierId = ProductDb.SupplierId;
                    Product.ProductName = ProductDb.ProductName;
                    Product.UnitPrice = ProductDb.UnitPrice;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Product;
        }

        public int generateSeries(int? yearsOrderParam = null)
        {
            int yearOrder = yearsOrderParam ?? DateTime.Today.Year;
            if (!_context.Orders.Any(c => c.OrderDate.Year == yearOrder))
            {
                return 1;
            }
            return _context.Orders.Max(c => c.Serie) + 1;
        }
        public string generateCodeOrder(long serieOrder, int? yearsOrderParam = null)
        {
            int yearOrder = yearsOrderParam ?? DateTime.Today.Year;
            string subStringTwoDigit = yearOrder.ToString().Substring(2, 2);
            var codeBase = "OR";
            return $"{codeBase}-{subStringTwoDigit}-{serieOrder.ToString().PadLeft(4, '0')}";
        }

        public bool SaveOrder(OrderDTO orderDTO)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                int SerieOrder = generateSeries();
                string CodeOrder = generateCodeOrder(SerieOrder);

                // Create Order
                Models.Order newOrder = new Models.Order();
                newOrder.CustomerId = orderDTO.CustomerId;
                newOrder.OrderDate = DateTime.Now;
                newOrder.OrderNumber = CodeOrder;
                newOrder.TotalAmount = getTotalAmount(orderDTO.ItemOrderItemDto);
                newOrder.Serie = SerieOrder;
                newOrder.Statu = true;
                newOrder.DateCreate = DateTime.Now;
                _context.Orders.Add(newOrder);
                var row = _context.SaveChanges();

                orderDTO.OrderNumber = CodeOrder;

                //Create Item Order
                for (int i = 0; i < orderDTO.ItemOrderItemDto.Count; i++)
                {
                    ItemOrderItemDto itemOrderDto = orderDTO.ItemOrderItemDto[i];
                    Models.OrderItem newOrderItem = new Models.OrderItem();
                    newOrderItem.OrderId = newOrder.OrderId;
                    newOrderItem.ProductId = itemOrderDto.ProductId;
                    newOrderItem.Quantity = itemOrderDto.Quantity;
                    //get product 
                    ProductDTO productDTO = getProductById(itemOrderDto.ProductId);
                    newOrderItem.UnitPrice = productDTO.UnitPrice;
                    newOrderItem.Statu = true;
                    newOrderItem.DateCreate = DateTime.Now;
                    _context.OrderItems.Add(newOrderItem);
                    _context.SaveChanges();
                }

                transaction.Commit();
                if (row > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public decimal getTotalAmount(List<ItemOrderItemDto> ItemOrderItemDto)
        {
            decimal TotalAmount = 0;

            for (int i = 0; i < ItemOrderItemDto.Count; i++)
            {
                ItemOrderItemDto itemOrderDto = ItemOrderItemDto[i];
                decimal Quantity = itemOrderDto?.Quantity ?? 0;
                decimal UnitPrice = itemOrderDto?.UnitPrice ?? 0;
                TotalAmount = TotalAmount + (Quantity * UnitPrice);
            }
            return TotalAmount;
        }

        public OrderDTO getOrderById(int IdOrder)
        {
            OrderDTO Order = new OrderDTO();
            try
            {
                Models.Order OrderDb = _context.Orders.FirstOrDefault(x => x.OrderId == IdOrder);
                if (OrderDb != null)
                {
                    Order.OrderNumber = OrderDb.OrderNumber;
                    Order.OrderDate = OrderDb.OrderDate;
                    Order.TotalAmount = OrderDb.TotalAmount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Order;
        }
    }
}
