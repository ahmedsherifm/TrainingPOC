using ECommerce.Core;
using ECommerce.Core.Constants;
using ECommerce.Business.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ECommerce.ViewModels
{
    public class LoginViewModel : BindableBase, INavigationAware
    {
        private string _username;
        private IDialogService _dialogService;
        private readonly IValidationService _validationService;
        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;

        public LoginViewModel(IDialogService dialogService, IValidationService validationService,
            IRegionManager regionManager, IUserService userService)
        {
            LoginCommand = new DelegateCommand(OnSubmit);
            _dialogService = dialogService;
            _validationService = validationService;
            _regionManager = regionManager;
            _userService = userService;
        }

        public string Username
        {
            get { return _username; }
            set
            { 
                SetProperty(ref _username, value); 
            }
        }

        public DelegateCommand LoginCommand { get; set; }

        private void OnSubmit()
        {
            if (!IsUsernameValid())
            {
                _dialogService.ShowMessageDialog("Username format is not valid.\n" +
                    "Please re-enter Username without spaces or special characters", null);

                return;
            }

            var userId = GetUserId();
            if(userId == -1)
            {
                _dialogService.ShowMessageDialog("Username not found", null);
                return;
            }

            Global.UserId = userId;
            Global.UserName = Username;

            _regionManager.RequestNavigate(Regions.MainRegion, ViewsNames.ContainerView);
            _regionManager.RequestNavigate(Regions.ContentRegion, ViewsNames.ProductsView);
        }

        private bool IsUsernameValid()
        {
            return _validationService.IsRegexValid(Username, RegularExpressions.LETTERS_AND_NUMBERS_ONLY_PATTERN);
        }

        private int GetUserId()
        {
            return _userService.GetUserIdByUsername(Username);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Username = "";
            Global.UserId = -1;
            Global.UserName = "";
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
