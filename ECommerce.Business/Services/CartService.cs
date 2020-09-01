using ECommerce.Core;
using ECommerce.DAL.Models;
using ECommerce.DAL.XMLManager;
using ECommerce.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ECommerce.Business.Services
{
    public class CartService : ICartSerivce
    {
        private readonly IXMLManager _xmlManager;

        IList<CartItem> CartItems { get; set; } = new List<CartItem>();
        List<CartItem> AllCartItems { get; set; } = new List<CartItem>();

        public CartService(IXMLManager xmlManager)
        {
            _xmlManager = xmlManager;

            var userId = (int)Global.UserId;
            GetCartItems(userId);
        }

        public bool AddCartItem(CartItem cartItem)
        {
            try
            {
                var existCartItem = CartItems.FirstOrDefault(ci => ci.ProductId == cartItem.ProductId &&
                                                                      ci.UserId == cartItem.UserId);
                if(existCartItem == null) 
                    CartItems.Add(cartItem);
                else
                    existCartItem.Quantity += cartItem.Quantity;

                AllCartItems.RemoveAll(ci => ci.UserId == cartItem.UserId);
                AllCartItems.AddRange(CartItems);

                var cartItems = new CartItems
                {
                    CartItemsList = new List<CartItem>(AllCartItems)
                };
                _xmlManager.Save("CartItems", cartItems);
                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
                return false;
            }
        }

        public ObservableCollection<CartItem> GetCartItems(int uid)
        {
            AllCartItems = _xmlManager.Load<CartItems>("CartItems").CartItemsList;
            CartItems = AllCartItems.Where(ci => ci.UserId == uid)
                        .Select(ci =>
                        {
                            if (ci.Quantity <= ci.Product.Quantity)
                                ci.IsAvailable = true;
                            else
                                ci.IsAvailable = false;

                            return ci;
                        }).ToList();

            return new ObservableCollection<CartItem>(CartItems);
        }

        public int GetNumberOfCartItems(int uid)
        {
            return CartItems.Where(ci => ci.UserId == uid)
                            .Sum(ci => ci.Quantity);
        }

        public bool RemoveItemFromCart(CartItem cartItem)
        {
            try
            {
                CartItems.Remove(cartItem);

                AllCartItems.RemoveAll(ci => ci.UserId == cartItem.UserId);
                AllCartItems.AddRange(CartItems);

                var cartItems = new CartItems
                {
                    CartItemsList = new List<CartItem>(AllCartItems)
                };
                _xmlManager.Save("CartItems", cartItems);
                return true;
            }
            catch (Exception x)
            {

                return false;
            }
        }

        public bool SubmitOrder()
        {
            try
            {
                CartItems.Clear();
                AllCartItems.RemoveAll(ci => ci.UserId == (int)Global.UserId);

                var cartItems = new CartItems
                {
                    CartItemsList = new List<CartItem>(AllCartItems)
                };
                _xmlManager.Save("CartItems", cartItems);
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
        }

    }
}
