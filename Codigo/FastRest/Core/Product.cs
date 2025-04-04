using System;
using System.Collections.Generic;

namespace Core
{
    public partial class Product
    {
        public Product()
        {
            Orderproducts = new HashSet<Orderproducts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int RestaurantId { get; set; }
        public int MenuCategoryId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Menucategory MenuCategory { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Orderproducts> Orderproducts { get; set; }
    }
}
