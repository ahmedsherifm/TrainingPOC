using ECommerce.Core;
using ECommerce.Core.Constants;
using ECommerce.Main.Models;
using ECommerce.Main.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ECommerce.Main.ViewModels
{
    public class ProductDetailsViewModel: BindableBase, INavigationAware
    {
        private Product product;
        private readonly IProductService _productService;
        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;
        private readonly ICartSerivce _cartSerivce;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;

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

        public ProductDetailsViewModel(IProductService productService, IRegionManager regionManager,
            IUserService userService, ICartSerivce cartSerivce, IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _productService = productService;
            _regionManager = regionManager;
            _userService = userService;
            _cartSerivce = cartSerivce;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;

            DecreaseStepperCommand = new DelegateCommand(OnDecreaseStepper, CanDecreaseStepper);
            IncreaseStepperCommand = new DelegateCommand(OnIncreaseStepper);
            AddToCartCommand = new DelegateCommand(OnAddToCart);
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

            var result = _cartSerivce.AddCartItem(cartItem);

            if (result)
            {
                _eventAggregator.GetEvent<MessageSentEvent>().Publish("Cart Item Added");
                _regionManager.RequestNavigate(Regions.MainRegion, ViewsNames.ProductsView);
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

        public DelegateCommand DecreaseStepperCommand { get; set; }
        public DelegateCommand IncreaseStepperCommand { get; set; }
        public DelegateCommand AddToCartCommand { get; set; }

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

                Product = _productService.GetProductById(productId);

                Stepper = 1;
            }
        }
    }
}
