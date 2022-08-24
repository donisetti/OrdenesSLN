using System;
using System.Collections.Generic;

#nullable disable

namespace OrdenesPrueba.Data.Models
{
    public partial class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public bool Statu { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? LastModificated { get; set; }
        public DateTime? DateDelete { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
