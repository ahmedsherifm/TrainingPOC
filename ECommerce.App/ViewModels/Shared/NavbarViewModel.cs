using ECommerce.Core;
using ECommerce.Core.Constants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ECommerce.Main.ViewModels.Shared
{
    public class NavbarViewModel: BindableBase
    {
        private readonly IRegionManager _regionManager;
        private string _username;
        private int _numberOfCartItems;
        
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public int NumberOfCartItems
        {
            get { return _numberOfCartItems; }
            set { SetProperty(ref _numberOfCartItems, value); }
        }

        public NavbarViewModel(IRegionManager regionManager)
        {
            Username = Global.UserName.ToString();
            CartCommand = new DelegateCommand(OnCartCommand);
            DotsCommand = new DelegateCommand(OnDotsCommand);
            _regionManager = regionManager;
        }

        public DelegateCommand CartCommand { get; private set; }
        public DelegateCommand DotsCommand { get; private set; }

        private void OnDotsCommand()
        {
            /// TODO: open context menu with options
            
            _regionManager.RequestNavigate(Regions.MainRegion, ViewsNames.ProductsView);
        }

        private void OnCartCommand()
        {
            _regionManager.RequestNavigate(Regions.MainRegion, ViewsNames.CartView);
        }
    }
}
