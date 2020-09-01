using ECommerce.Core.Constants;
using ECommerce.DAL.XMLManager;
using ECommerce.Business.Services;
using ECommerce.Business.Interfaces;
using ECommerce.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ECommerce
{
    public class AppModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public AppModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(Regions.MainRegion, typeof(LoginView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // navigation
            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterForNavigation<ProductsView>();
            containerRegistry.RegisterForNavigation<ProductDetailsView>();
            containerRegistry.RegisterForNavigation<CartView>();

            // services
            containerRegistry.RegisterSingleton<IValidationService, ValidationService>();
            containerRegistry.RegisterSingleton<IProductService, ProductService>();
            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IXMLManager, XMLManager>();
            containerRegistry.RegisterSingleton<ICartSerivce, CartService>();
        }
    }
}
