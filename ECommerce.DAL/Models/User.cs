using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ECommerce.DAL.Models
{
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Users
    {

        private List<User> _usersList;

        /// <remarks/>
        [XmlElementAttribute("User")]
        public List<User> UsersList
        {
            get
            {
                return _usersList;
            }
            set
            {
                _usersList = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class User
    {

        private int _id;

        private string _username;

        /// <remarks/>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <remarks/>
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }
    }


}
