using System;
using System.Collections.Generic;

#nullable disable

namespace PedidosPOC.Data.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool IsDiscontinued { get; set; }
        public bool Statu { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? LastModificated { get; set; }
        public DateTime? DateDelete { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
