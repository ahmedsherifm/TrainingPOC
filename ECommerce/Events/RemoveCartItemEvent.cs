using ECommerce.DAL.Models;
using Prism.Events;

namespace ECommerce.Events
{
    public class RemoveCartItemEvent: PubSubEvent<CartItem>
    {
    }
}
