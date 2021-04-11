using ECommerce.Core;
using ECommerce.Core.Constants;
using ECommerce.DAL.Models;
using ECommerce.Business.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using ECommerce.Events;

namespace ECommerce.ViewModels
{
    public class CartViewModel: BindableBase, INavigationAware
    {
        private readonly ICartSerivce _cartSerivce;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        ObservableCollection<CartItem> cartItems;

        public ObservableCollection<CartItem> CartItems
        {
            get => cartItems;

            set { SetProperty(ref cartItems, value); }
        }

        bool isSubmitEnabled;

        public bool IsSubmitEnabled
        {
            get => isSubmitEnabled;

            set { SetProperty(ref isSubmitEnabled, value); }
        }

        public CartViewModel(ICartSerivce cartSerivce, IDialogService dialogService, 
            IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _cartSerivce = cartSerivce;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            SubmitCommand = new DelegateCommand(OnSubmit);
            DeleteCommand = new DelegateCommand<CartItem>(OnDeleteItem);
        }

        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand<CartItem> DeleteCommand { get; private set; }

        private void OnSubmit()
        {
            IsSubmitEnabled = false;

            if (IsOutOfOrder())
                _dialogService.ShowMessageDialog("Some items are out of order, it will be in your cart until you delete it.", null);

            var isSubmitted = _cartSerivce.SubmitOrder();
            if (isSubmitted)
            {
                _eventAggregator.GetEvent<MessageSentEvent>().Publish("Order Submitted Successfully");
                _dialogService.ShowMessageDialog("Order Submitted Successfully", null);
                _regionManager.RequestNavigate(Regions.ContentRegion, ViewsNames.ProductsView);
            }
            else
            {
                _dialogService.ShowMessageDialog("Something Went Wrong", null);
                IsSubmitEnabled = true;
            }
        }

        private void OnDeleteItem(CartItem cartitem)
        {
            var isDeleted = _cartSerivce.RemoveItemFromCart(cartitem);

            if (isDeleted)
            {
                _eventAggregator.GetEvent<RemoveCartItemEvent>().Publish(cartitem);
                CartItems.Remove(cartitem);
                CheckCartItems();
            }
            else
            {
                _dialogService.ShowMessageDialog("Couldn't Delete Item", null);
            }
        }

        private bool IsOutOfOrder()
        {
            return CartItems.Any(ci => !ci.IsAvailable);
        }

        private void CheckCartItems()
        {
            bool isOutOfOrder = IsOutOfOrder();

            if (CartItems.Count == 0)
            {
                _dialogService.ShowMessageDialog("Cart is Empty", null);
                IsSubmitEnabled = false;
            }
            else if (CartItems.Count > 0 && isOutOfOrder)
            {
                _dialogService.ShowMessageDialog("Some items are out of order, it will be in your cart until you delete it.", null);
                IsSubmitEnabled = false;
            }
            else
            {
                IsSubmitEnabled = true;
            }
        }

        private void LoadCartItems()
        {
            IsSubmitEnabled = false;

            var userId = (int)Global.UserId;

            CartItems = _cartSerivce.GetCartItems(userId);

            CheckCartItems();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadCartItems();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
