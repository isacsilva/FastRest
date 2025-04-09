using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class OrdertableDTO
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public required string Status { get; set; }
        public required string ConsumptionMethod { get; set; }
        public int RestaurantId { get; set; }

    }
}
