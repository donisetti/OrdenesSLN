using Microsoft.AspNetCore.Mvc;
using OrdenesPrueba.Data.DTO.Order;
using OrdenesPrueba.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrdenesPrueba.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ListOrderDto listOrderDto = await orderRepository.getAllOrder();

                if (listOrderDto != null)
                {
                    return View(listOrderDto);
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return View();
        }

        public async Task<IActionResult> Create()
        {
            OrderDTO NewOrder = new OrderDTO();
            List<ItemOrderItemDto> ItemOrden = new List<ItemOrderItemDto>();
            NewOrder.ItemOrderItemDto = ItemOrden;
            long SerieOrder = orderRepository.generateSeries();
            string CodigoOrder = orderRepository.generateCodeOrder(SerieOrder);
            NewOrder.OrderNumber = CodigoOrder;

            ViewData["CustomerId"] = new SelectList(orderRepository.getAllCustomer(), "CustomerId", "CustomerName");
            ViewData["ProductId"] = new SelectList(orderRepository.getAllProduct(), "ProductId", "ProductName");
            return View(NewOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderDTO orderDTO)
        {
            ViewData["CustomerId"] = new SelectList(orderRepository.getAllCustomer(), "CustomerId", "CustomerName");
            ViewData["ProductId"] = new SelectList(orderRepository.getAllProduct(), "ProductId", "ProductName");

            if (orderDTO.ItemOrderItemDto.Count < 1)
            {
                ModelState.AddModelError("ItemOrderItemDto", "Please Insert Item to Order");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    bool ResultSave = orderRepository.SaveOrder(orderDTO);
                    if (ResultSave)
                    {
                        orderDTO.Process = true;
                        orderDTO.Success = true;
                        orderDTO.Mesage = "Order: " + orderDTO.OrderNumber + " Created Success ";
                        List<ItemOrderItemDto> ItemOrden = new List<ItemOrderItemDto>();
                        long SerieOrder = orderRepository.generateSeries();
                        string CodigoOrder = orderRepository.generateCodeOrder(SerieOrder);
                        orderDTO.OrderNumber = CodigoOrder;
                        orderDTO.ItemOrderItemDto = ItemOrden;
                    }
                    else
                    {
                        orderDTO.Process = false;
                        orderDTO.Success = false;
                        orderDTO.Mesage = "Alert Error, Order Don't Created";
                    }
                }
                
            }
            catch (Exception ex)
            {
                orderDTO.Process = false;
                orderDTO.Success = false;
                orderDTO.Mesage = ex.Message;
                throw;
            }
           
            return View(orderDTO);
        }

        public async Task<IActionResult> Checkout(int id)
        {
            OrderDTO NewOrder =  orderRepository.getOrderById(id);
            return View(NewOrder);
        }

        public JsonResult GetDetailProduct(int IdProduct)
        {
            try
            {
                return Json(new { ProductDetail = orderRepository.getProductById(IdProduct) });
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
