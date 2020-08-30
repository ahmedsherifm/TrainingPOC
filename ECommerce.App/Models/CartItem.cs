using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Main.Models
{
    public class CartItem
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public Product Product { get; set; }
    }
}
