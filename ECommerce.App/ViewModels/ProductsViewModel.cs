using ECommerce.Core;
using ECommerce.Main.Models;
using ECommerce.Main.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;

namespace ECommerce.Main.ViewModels
{
    public class ProductsViewModel: BindableBase, INavigationAware
    {
        private IList<Product> _products;
        public IList<Product> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { SetProperty(ref _selectedProduct, value); }
        }

        private readonly IProductService _productService;
        private readonly IDialogService _dialogService;

        public ProductsViewModel(IProductService productService, IDialogService dialogService)
        {
            _productService = productService;
            _dialogService = dialogService;
            SelectProductCommand = new DelegateCommand(OnSelectProductCommand);
        }

        private void OnSelectProductCommand()
        {
            _dialogService.ShowMessageDialog($"product id: {SelectedProduct.Id}", null);
        }

        public DelegateCommand SelectProductCommand { get; private set; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Products = _productService.GetProductsOrderedByPrice();
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
