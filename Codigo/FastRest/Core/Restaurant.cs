using System;
using System.Collections.Generic;

namespace Core
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Menucategory = new HashSet<Menucategory>();
            Ordertable = new HashSet<Ordertable>();
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string AvatarImageUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Menucategory> Menucategory { get; set; }
        public virtual ICollection<Ordertable> Ordertable { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
