using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace ECommerce.Main.Models
{
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class CartItems
    {
        private List<CartItem> _cartItemsList;

        /// <remarks/>
        [XmlElementAttribute("CartItem")]
        public List<CartItem> CartItemsList
        {
            get
            {
                return _cartItemsList;
            }
            set
            {
                _cartItemsList = value;
            }
        }
    }

    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class CartItem
    {
        private int userId;
        private int productId;
        private int quantity;
        private bool isAvailable;
        private Product product;

        public int UserId { get => userId; set => userId = value; }
        public int ProductId { get => productId; set => productId = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        [XmlIgnore]
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        public Product Product { get => product; set => product = value; }
    }
}
