using ECommerce.DAL.Models;
using System.Collections.Generic;

namespace ECommerce.Repo.Interfaces
{
    public interface ICartRepository
    {
        IEnumerable<CartItem> LoadCartItems();

        bool SaveCartItems(List<CartItem> cartItems);
    }
}
