using System;
using System.Collections.Generic;

namespace Core
{
    public partial class Menucategory
    {
        public Menucategory()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
