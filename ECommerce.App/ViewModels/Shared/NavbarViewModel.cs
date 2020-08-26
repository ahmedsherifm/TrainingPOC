using ECommerce.Core;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Main.ViewModels.Shared
{
    public class NavbarViewModel: BindableBase
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public NavbarViewModel()
        {
            Username = Global.UserName.ToString();
        }
    }
}
