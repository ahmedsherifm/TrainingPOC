using Prism.Ioc;
using Prism.DryIoc;
using System.Windows;
using ECommerce.Views;
using Prism.Modularity;
using ECommerce.Main;
using ECommerce.Dialogs;

namespace ECommerce
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<MessageDialog, MessageDialogViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AppModule>();
        }
    }
}
