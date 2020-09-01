using ECommerce.DAL.Models;
using ECommerce.DAL.XMLManager;
using ECommerce.Repo.Interfaces;
using System.Collections.Generic;

namespace ECommerce.Repo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IXMLManager _xmlManager;

        public UserRepository(IXMLManager xmlManager)
        {
            _xmlManager = xmlManager;
        }

        public IEnumerable<User> LoadUsers()
        {
            var users = _xmlManager.Load<Users>("Users");
            return users.UsersList;
        }
    }
}
