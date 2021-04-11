using ECommerce.Core;
using ECommerce.Core.Constants;
using ECommerce.DAL.Models;
using ECommerce.Business.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using ECommerce.Events;
using System;

namespace ECommerce.ViewModels
{
    public class ProductDetailsViewModel: BindableBase, INavigationAware
    {
        private Product product;
        private readonly IProductService _productService;
        private readonly IRegionManager _regionManager;
        private readonly ICartSerivce _cartSerivce;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;

        public Product Product
        {
            get { return product; }
            set { SetProperty(ref product, value); }
        }

        private int _stepper = 1;
        public int Stepper
        {
            get { return _stepper; }
            set 
            { 
                SetProperty(ref _stepper, value);
                DecreaseStepperCommand.RaiseCanExecuteChanged();
            }
        }

        private string _currentImageUrl;
        public string CurrentImageUrl
        {
            get { return _currentImageUrl; }
            set { SetProperty(ref _currentImageUrl, value); }
        }

        private int _currentImageUrlIndex = 0;
        public int CurrentImageUrlIndex
        {
            get { return _currentImageUrlIndex; }
            set { SetProperty(ref _currentImageUrlIndex, value); }
        }

        public ProductDetailsViewModel(IProductService productService, IRegionManager regionManager, 
            ICartSerivce cartSerivce, IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _productService = productService;
            _regionManager = regionManager;
            _cartSerivce = cartSerivce;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            DecreaseStepperCommand = new DelegateCommand(OnDecreaseStepper, CanDecreaseStepper);
            IncreaseStepperCommand = new DelegateCommand(OnIncreaseStepper);
            AddToCartCommand = new DelegateCommand(OnAddToCart);
            NextImageCommand = new DelegateCommand(()=>OnChangeImage("next"), () => CanChangeImage("next"));
            PrevImageCommand = new DelegateCommand(()=>OnChangeImage("prev"), () => CanChangeImage("prev"));
        }

        public DelegateCommand DecreaseStepperCommand { get; set; }
        public DelegateCommand IncreaseStepperCommand { get; set; }
        public DelegateCommand AddToCartCommand { get; set; }
        public DelegateCommand NextImageCommand { get; set; }
        public DelegateCommand PrevImageCommand { get; set; }

        private void OnChangeImage(string direction)
        {
            switch (direction)
            {
                case "next":
                    CurrentImageUrlIndex++;
                    break;
                case "prev":
                    CurrentImageUrlIndex--;
                    break;
            }

            CurrentImageUrl = Product.Images[CurrentImageUrlIndex];
            RaiseCanExecuteChangedForImagesCommands();
        }

        private bool CanChangeImage(string direction)
        {
            if (Product == null) return false;

            return direction switch
            {
                "next" => CurrentImageUrlIndex < Product.Images.Count - 1,
                "prev" => CurrentImageUrlIndex > 0,
                _ => false
            };
        }

        private bool CanDecreaseStepper()
        {
            if (Stepper < 2)
                return false;

            return true;
        }

        private void OnAddToCart()
        {
            var cartItem = new CartItem
            {
                UserId = (int)Global.UserId,
                ProductId = Product.Id,
                Product = Product,
                Quantity = Stepper
            };

            var isAdded = _cartSerivce.AddCartItem(cartItem);

            if (isAdded)
            {
                _eventAggregator.GetEvent<MessageSentEvent>().Publish("Cart Updated Successfully");
                _dialogService.ShowMessageDialog("Cart Updated Successfully", null);
                _regionManager.RequestNavigate(Regions.ContentRegion, ViewsNames.ProductsView);
            }
            else
            {
                _dialogService.ShowMessageDialog("Something went wrong", null);
            }
        }

        private void OnIncreaseStepper()
        {
            Stepper++;
        }

        private void OnDecreaseStepper()
        {
            Stepper--;
        }

        private void RaiseCanExecuteChangedForImagesCommands()
        {
            NextImageCommand.RaiseCanExecuteChanged();
            PrevImageCommand.RaiseCanExecuteChanged();
        }

        private void InitData(int productId)
        {
            Product = _productService.GetProductById(productId);
            Stepper = 1;
            CurrentImageUrlIndex = 0;
            CurrentImageUrl = Product.Images.Count > 0 ? Product.Images[CurrentImageUrlIndex] : Product.ImagePosterUrl;
            RaiseCanExecuteChangedForImagesCommands();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("productId"))
            {
                var productId = navigationContext.Parameters.GetValue<int>("productId");
                InitData(productId);
            }
        }
    }
}
