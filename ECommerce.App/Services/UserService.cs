using ECommerce.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Main.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>();

        private User _currentUser;

        public int GetUserIdByUsername(string username)
        {
            LoadUsersFromXML();

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
            XMLManager manager = new XMLManager();
            _users = manager.LoadAll<User>("Users").ToList();
        }
    }
}
