using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Main.ViewModels
{
    public class ProductDetailsViewModel: BindableBase, INavigationAware
    {
        private int _productId;
        public int ProductId
        {
            get { return _productId; }
            set { SetProperty(ref _productId, value); }
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
                ProductId = navigationContext.Parameters.GetValue<int>("productId");
        }
    }
}
