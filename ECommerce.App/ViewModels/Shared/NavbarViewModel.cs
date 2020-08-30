using ECommerce.Core;
using ECommerce.Core.Constants;
using ECommerce.Main.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ECommerce.Main.ViewModels.Shared
{
    public class NavbarViewModel: BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;
        private readonly ICartSerivce _cartSerivce;
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

        public NavbarViewModel(IRegionManager regionManager, IUserService userService, ICartSerivce cartSerivce)
        {
            Username = Global.UserName.ToString();
            CartCommand = new DelegateCommand(OnCartCommand);
            DotsCommand = new DelegateCommand(OnDotsCommand);
            _regionManager = regionManager;
            _userService = userService;
            _cartSerivce = cartSerivce;

            OnAppearing();
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

        private void OnAppearing()
        {
            var userId = _userService.GetUserIdByUsername(Username);
            NumberOfCartItems = _cartSerivce.GetNumberOfCartItems(userId);
        }
    }
}
