using ECommerce.DAL.Models;
using ECommerce.DAL.XMLManager;
using ECommerce.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IXMLManager _xmlManager;
        private List<User> _users = new List<User>();
        private User _currentUser;

        public UserService(IXMLManager xmlManager)
        {
            _xmlManager = xmlManager;
            LoadUsersFromXML();
        }

        public int GetUserIdByUsername(string username)
        {
            if (IsUserExists(username))
                return _currentUser.Id;

            return -1;
        }

        public bool IsUserExists(string username)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                _currentUser = user;
                return true;
            }

            return false;
        }

        private void LoadUsersFromXML()
        {
            var users = _xmlManager.Load<Users>("Users");
            _users = users.UsersList;
        }
    }
}
