using ECommerce.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ECommerce.Main.ViewModels.Shared
{
    public class NavbarViewModel: BindableBase
    {
        private readonly IDialogService _dialogService;
        private string _username;
        private int _numberOfCartItems;
        
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public int NumberOfCartItems
        {
            get { return _numberOfCartItems; }
            set { SetProperty(ref _numberOfCartItems, value); }
        }

        public NavbarViewModel(IDialogService dialogService)
        {
            Username = Global.UserName.ToString();
            CartCommand = new DelegateCommand(OnCartCommand);
            DotsCommand = new DelegateCommand(OnDotsCommand);
            _dialogService = dialogService;
        }

        public DelegateCommand CartCommand { get; private set; }
        public DelegateCommand DotsCommand { get; private set; }

        private void OnDotsCommand()
        {
            _dialogService.ShowMessageDialog("Options Dialog", null);
        }

        private void OnCartCommand()
        {
            _dialogService.ShowMessageDialog("Open Cart Page", null);
        }
    }
}
