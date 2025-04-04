using System;
using System.Collections.Generic;

namespace Core
{
    public partial class Ordertable
    {
        public Ordertable()
        {
            Orderproducts = new HashSet<Orderproducts>();
        }

        public int Id { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string ConsumptionMethod { get; set; }
        public int RestaurantId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Orderproducts> Orderproducts { get; set; }
    }
}
