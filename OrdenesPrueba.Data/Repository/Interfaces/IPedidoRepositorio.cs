using PedidosPOC.Data.DTO.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PedidosPOC.Data.Repository.Interfaces;

public interface IPedidoRepositorio
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
