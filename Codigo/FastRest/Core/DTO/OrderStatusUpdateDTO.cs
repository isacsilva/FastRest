﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class OrderStatusUpdateDTO
    {
        public int IdPedido { get; set; }
        public required string Status { get; set; }
    }
}
