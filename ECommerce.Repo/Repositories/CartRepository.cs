using ECommerce.DAL.Models;
using ECommerce.DAL.XMLManager;
using ECommerce.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Repo.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IXMLManager _xmlManager;

        public CartRepository(IXMLManager xmlManager)
        {
            _xmlManager = xmlManager;
        }

        public IEnumerable<CartItem> LoadCartItems()
        {
            var cartItems = _xmlManager.Load<CartItems>("CartItems");
            return cartItems.CartItemsList;
        }

        public bool SaveCartItems(List<CartItem> cartItems)
        {
            try
            {
                var data = new CartItems
                {
                    CartItemsList = new List<CartItem>(cartItems)
                };
                _xmlManager.Save("CartItems", data);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
