using ECommerce.Core.Constants;
using ECommerce.Main.Models;
using ECommerce.Main.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
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
        private readonly IRegionManager _regionManager;

        public ProductsViewModel(IProductService productService, IRegionManager regionManager)
        {
            _productService = productService;
            _regionManager = regionManager;
            SelectProductCommand = new DelegateCommand(OnSelectProductCommand);
        }

        private void OnSelectProductCommand()
        {
            if (SelectedProduct == null)
                return;

            var param = new NavigationParameters();
            param.Add("productId", SelectedProduct.Id);

            _regionManager.RequestNavigate(Regions.MainRegion, ViewsNames.ProductDetailsView, param);
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
