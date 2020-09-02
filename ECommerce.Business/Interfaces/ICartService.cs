using ECommerce.DAL.Models;
using System.Collections.ObjectModel;

namespace ECommerce.Business.Interfaces
{
    public interface ICartSerivce
    {
        ObservableCollection<CartItem> GetCartItems(int uid);

        bool AddCartItem(CartItem cartItem);

        int GetNumberOfCartItems(int uid);

        bool RemoveItemFromCart(CartItem cartItem);

        bool SubmitOrder();

        void LoadCartItems();
    }
}
