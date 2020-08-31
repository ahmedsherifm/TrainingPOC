using ECommerce.Main.Models;
using System.Collections.ObjectModel;

namespace ECommerce.Main.Services
{
    public interface ICartSerivce
    {
        ObservableCollection<CartItem> GetCartItems(int uid);

        bool AddCartItem(CartItem cartItem);

        int GetNumberOfCartItems(int uid);

        bool RemoveItemFromCart(CartItem cartItem);

        bool SubmitOrder();
    }
}
