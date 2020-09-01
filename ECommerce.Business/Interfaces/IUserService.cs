using ECommerce.DAL.Models;

namespace ECommerce.Business.Interfaces
{
    public interface IUserService
    {
        bool IsUserExists(string username);

        int GetUserIdByUsername(string username);
    }
}
