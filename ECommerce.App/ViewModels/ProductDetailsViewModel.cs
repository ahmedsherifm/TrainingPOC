using ECommerce.Core.Constants;
using ECommerce.Main.Models;
using ECommerce.Main.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace ECommerce.Main.ViewModels
{
    public class ProductDetailsViewModel: BindableBase, INavigationAware
    {
        private Product product;
        private readonly IProductService _productService;
        private readonly IRegionManager _regionManager;

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

        public ProductDetailsViewModel(IProductService productService, IRegionManager regionManager)
        {
            _productService = productService;
            _regionManager = regionManager;
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
            _regionManager.RequestNavigate(Regions.MainRegion, ViewsNames.ProductsView);
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
