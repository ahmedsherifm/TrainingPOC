using ECommerce.Core;
using ECommerce.DAL.Models;
using ECommerce.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ECommerce.Repo.Interfaces;

namespace ECommerce.Business.Services
{
    public class CartService : ICartSerivce
    {
        private readonly ICartRepository _cartRepository;

        List<CartItem> CartItems { get; set; } = new List<CartItem>();
        List<CartItem> AllCartItems { get; set; } = new List<CartItem>();

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;

            AllCartItems = _cartRepository.LoadCartItems().ToList();

            var userId = (int)Global.UserId;
            GetCartItems(userId);
        }

        public bool AddCartItem(CartItem cartItem)
        {
            var existCartItem = CartItems.FirstOrDefault(ci => ci.ProductId == cartItem.ProductId &&
                                                                  ci.UserId == cartItem.UserId);
            if (existCartItem == null)
                CartItems.Add(cartItem);
            else
                existCartItem.Quantity += cartItem.Quantity;

            AllCartItems.RemoveAll(ci => ci.UserId == cartItem.UserId);
            AllCartItems.AddRange(CartItems);

            return _cartRepository.SaveCartItems(AllCartItems);
        }

        public ObservableCollection<CartItem> GetCartItems(int uid)
        {
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
            CartItems.Remove(cartItem);

            AllCartItems.RemoveAll(ci => ci.UserId == cartItem.UserId);
            AllCartItems.AddRange(CartItems);

            return _cartRepository.SaveCartItems(AllCartItems);
        }

        public bool SubmitOrder()
        {
            CartItems.Clear();
            AllCartItems.RemoveAll(ci => ci.UserId == (int)Global.UserId);

            return _cartRepository.SaveCartItems(AllCartItems);
        }

    }
}
