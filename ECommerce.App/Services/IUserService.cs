using ECommerce.Main.Models;

namespace ECommerce.Main.Services
{
    public interface IUserService
    {
        bool IsUserExists(string username);

        int GetUserIdByUsername(string username);
    }
}
