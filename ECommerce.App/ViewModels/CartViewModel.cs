using ECommerce.Core;
using ECommerce.Main.Models;
using ECommerce.Main.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ECommerce.Main.ViewModels
{
    public class CartViewModel: BindableBase, INavigationAware
    {
        private readonly ICartSerivce _cartSerivce;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
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

        public CartViewModel(ICartSerivce cartSerivce, IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _cartSerivce = cartSerivce;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            SubmitCommand = new DelegateCommand(OnSubmit);
            DeleteCommand = new DelegateCommand<CartItem>(OnDeleteItem);
        }

        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand<CartItem> DeleteCommand { get; private set; }

        private void OnSubmit()
        {
        }

        private void OnDeleteItem(CartItem cartitem)
        {
            var isDeleted = _cartSerivce.RemoveItemFromCart(cartitem);

            if (isDeleted)
            {
                _eventAggregator.GetEvent<MessageSentEvent<CartItem>>().Publish(cartitem);
                CartItems.Remove(cartitem);
                CheckCartItems();
            }
            else
            {
                _dialogService.ShowMessageDialog("Couldn't Delete Item", null);
            }
        }

        private void CheckCartItems()
        {
            bool isOutOfOrder = CartItems.Any(ci => !ci.IsAvailable);

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
