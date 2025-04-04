using System;
using System.Collections.Generic;

namespace Core
{
    public partial class Orderproducts
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Ordertable Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
