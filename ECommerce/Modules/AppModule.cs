using ECommerce.Core.Constants;
using ECommerce.DAL.XMLManager;
using ECommerce.Business.Services;
using ECommerce.Business.Interfaces;
using ECommerce.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ECommerce.Repo.Interfaces;
using ECommerce.Repo.Repositories;
using ECommerce.Views.Shared;

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
            _regionManager.RegisterViewWithRegion(Regions.NavbarRegion, typeof(NavbarView));
            _regionManager.RegisterViewWithRegion(Regions.ContentRegion, typeof(ProductsView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // navigation
            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterForNavigation<ContainerView>();
            containerRegistry.RegisterForNavigation<ProductsView>();
            containerRegistry.RegisterForNavigation<ProductDetailsView>();
            containerRegistry.RegisterForNavigation<CartView>();

            // services
            containerRegistry.RegisterSingleton<IValidationService, ValidationService>();
            containerRegistry.RegisterSingleton<IProductService, ProductService>();
            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IXMLManager, XMLManager>();
            containerRegistry.RegisterSingleton<ICartSerivce, CartService>();

            // repositories
            containerRegistry.RegisterSingleton<IUserRepository, UserRepository>();
            containerRegistry.RegisterSingleton<IProductRepository, ProductRepository>();
            containerRegistry.RegisterSingleton<ICartRepository, CartRepository>();
        }
    }
}
