using System;
using System.Collections.Generic;

#nullable disable

namespace PedidosPOC.Data.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string OrderNumber { get; set; }
        public int Serie { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Statu { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? LastModificated { get; set; }
        public DateTime? DateDelete { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
