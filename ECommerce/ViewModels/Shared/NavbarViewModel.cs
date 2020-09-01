﻿using ECommerce.Core;
using ECommerce.Core.Constants;
using ECommerce.DAL.Models;
using ECommerce.Business.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace ECommerce.ViewModels.Shared
{
    public class NavbarViewModel: BindableBase, INavigationAware
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
            CartCommand = new DelegateCommand(OnCartCommand);
            HomeCommand = new DelegateCommand(OnHomeCommand);
            FilterCommand = new DelegateCommand(OnFilterCommand);
            LogoutCommand = new DelegateCommand(OnLogoutCommand);

            _regionManager = regionManager;
            _cartSerivce = cartSerivce;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<MessageSentEvent<CartItem>>().Subscribe(OnRemoveItemFromCart);
            _eventAggregator.GetEvent<MessageSentEvent<string>>().Subscribe(OnAddCartItem);
        }

        private void Init()
        {
            Username = Global.UserName.ToString();
            _cartSerivce.LoadCartItems();

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
        public DelegateCommand HomeCommand { get; private set; }
        public DelegateCommand FilterCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }

        private void OnHomeCommand()
        {
            /// TODO: open context menu with options
            
            _regionManager.RequestNavigate(Regions.ContentRegion, ViewsNames.ProductsView);
        }

        private void OnCartCommand()
        {
            _regionManager.RequestNavigate(Regions.ContentRegion, ViewsNames.CartView);
        }

        private void OnLogoutCommand()
        {
            _regionManager.RequestNavigate(Regions.MainRegion, ViewsNames.LoginView);
        }

        private void OnFilterCommand()
        {
        }

        private void LoadNumberOfCartItems()
        {
            var userId = (int)Global.UserId;
            NumberOfCartItems = _cartSerivce.GetNumberOfCartItems(userId);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Init();
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
