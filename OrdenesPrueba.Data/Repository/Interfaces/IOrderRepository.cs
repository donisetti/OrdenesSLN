using OrdenesPrueba.Data.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdenesPrueba.Data.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<ListOrderDto> getAllOrder();
        List<ItemCustomerDto> getAllCustomer();
        List<ItemProductDto> getAllProduct();
        ProductDTO getProductById(int IdProduct);
        int generateSeries(int? yearsOrderParam = null);
        string generateCodeOrder(long serieOrder, int? yearsOrderParam = null);
        bool SaveOrder(OrderDTO orderDTO);
        OrderDTO getOrderById(int IdOrder);
    }
}
