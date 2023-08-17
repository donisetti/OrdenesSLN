using System;
using System.Collections.Generic;

#nullable disable

namespace PedidosPOC.Data.Models
{
    public partial class Supplier
    {
        public int SupplierId { get; set; }
        public string CopanyName { get; set; }
        public string Phone { get; set; }
        public bool Statu { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? LastModificated { get; set; }
        public DateTime? DateDelete { get; set; }

        public virtual Product Product { get; set; }
    }
}
