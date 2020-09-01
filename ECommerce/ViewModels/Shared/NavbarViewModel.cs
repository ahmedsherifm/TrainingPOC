using ECommerce.Core;
using ECommerce.Core.Constants;
using ECommerce.DAL.Models;
using ECommerce.Business.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace ECommerce.ViewModels.Shared
{
    public class NavbarViewModel: BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly ICartSerivce _cartSerivce;
        private readonly IEventAggregator _eventAggregator;
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

        public NavbarViewModel(IRegionManager regionManager, ICartSerivce cartSerivce, IEventAggregator eventAggregator)
        {
            Username = Global.UserName.ToString();

            CartCommand = new DelegateCommand(OnCartCommand);
            DotsCommand = new DelegateCommand(OnDotsCommand);

            _regionManager = regionManager;
            _cartSerivce = cartSerivce;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<MessageSentEvent<CartItem>>().Subscribe(OnRemoveItemFromCart);
            _eventAggregator.GetEvent<MessageSentEvent<string>>().Subscribe(OnAddCartItem);

            LoadNumberOfCartItems();
        }

        private void OnAddCartItem(string message)
        {
            LoadNumberOfCartItems();
        }

        private void OnRemoveItemFromCart(CartItem cartItem)
        {
            NumberOfCartItems -= cartItem.Quantity;
        }

        public DelegateCommand CartCommand { get; private set; }
        public DelegateCommand DotsCommand { get; private set; }

        private void OnDotsCommand()
        {
            /// TODO: open context menu with options
            
            _regionManager.RequestNavigate(Regions.ContentRegion, ViewsNames.ProductsView);
        }

        private void OnCartCommand()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, ViewsNames.CartView);
        }

        private void LoadNumberOfCartItems()
        {
            var userId = (int)Global.UserId;
            NumberOfCartItems = _cartSerivce.GetNumberOfCartItems(userId);
        }
    }
}
