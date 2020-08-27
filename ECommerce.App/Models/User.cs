using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ECommerce.Main.Models
{
    [XmlRoot("ArrayOfUser")]
    public class UsersList
    {
        public UsersList()
        {
            Users = new List<User>();
        }

        [XmlElement("User")]
        public List<User> Users { get; set; }
    }

    public class User
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Username")]
        public string Username { get; set; }
    }
}
