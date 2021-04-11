using ECommerce.Core.Constants;
using ECommerce.DAL.Models;
using ECommerce.Business.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using Prism.Events;
using ECommerce.Events;
using System;
using Prism.Services.Dialogs;
using ECommerce.Dialogs;
using ECommerce.DTOs;
using System.Linq;
using System.Collections.ObjectModel;

namespace ECommerce.ViewModels
{
    public class ProductsViewModel: BindableBase, INavigationAware
    {
        private readonly IProductService _productService;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
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

        public ProductFilterDTO ProductFilter { get; set; } = new ProductFilterDTO();

        public ProductsViewModel(IProductService productService, IRegionManager regionManager,
            IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _productService = productService;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;

            SelectProductCommand = new DelegateCommand(OnSelectProductCommand);

            _eventAggregator.GetEvent<ShowFilterPopupEvent>().Subscribe(ShowFilterPopup);
        }

        private void ShowFilterPopup()
        {
            var param = new DialogParameters();
            param.Add("productFilter", ProductFilter);

            _dialogService.ShowDialog(nameof(FilterDialog), param, FilterProucts);
        }

        private void FilterProucts(IDialogResult res)
        {
            if(res.Result == ButtonResult.OK)
            {
                ProductFilter = res.Parameters.GetValue<ProductFilterDTO>("productFilter");
                var productsFiltered = _productService.GetProductsOrderedByPriceAndFiltered(ProductFilter.MinPrice, ProductFilter.MaxPrice);
                Products = new ObservableCollection<Product>(productsFiltered);
            }
        }

        private void OnSelectProductCommand()
        {
            if (SelectedProduct == null)
                return;

            var param = new NavigationParameters();
            param.Add("productId", SelectedProduct.Id);

            _regionManager.RequestNavigate(Regions.ContentRegion, ViewsNames.ProductDetailsView, param);
        }

        public DelegateCommand SelectProductCommand { get; private set; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Products = new ObservableCollection<Product>(_productService.GetProductsOrderedByPrice());

            _eventAggregator.GetEvent<ShowFilterMenuItemEvent>().Publish(true);
            ProductFilter.MinPrice = (int)Products.Min(p => p.Price);
            ProductFilter.MaxPrice = (int)Products.Max(p => p.Price);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            SelectedProduct = null;
         
            _eventAggregator.GetEvent<ShowFilterMenuItemEvent>().Publish(false);
        }
    }
}
