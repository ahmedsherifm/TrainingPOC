using ECommerce.Main.Services;
using ECommerce.Main.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ECommerce.Main
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
            _regionManager.RegisterViewWithRegion("MainRegion", typeof(LoginView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IValidationService, ValidationService>();
        }
    }
}
