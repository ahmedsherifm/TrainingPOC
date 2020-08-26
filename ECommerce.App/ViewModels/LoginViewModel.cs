using ECommerce.Core;
using ECommerce.Core.Constants;
using ECommerce.Main.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ECommerce.Main.ViewModels
{
    public class LoginViewModel: BindableBase, INavigationAware
    {
        private string _username;
        private IDialogService _dialogService;
        private readonly IValidationService _validationService;
        private readonly IRegionManager _regionManager;

        public LoginViewModel(IDialogService dialogService, IValidationService validationService, IRegionManager regionManager)
        {
            LoginCommand = new DelegateCommand(OnSubmit);
            _dialogService = dialogService;
            _validationService = validationService;
            _regionManager = regionManager;
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
            if (!CheckUsername())
            {
                _dialogService.ShowMessageDialog("Username format is not valid.\n" +
                    "Please re-enter Username without spaces or special characters", null);

                return;
            }

            _dialogService.ShowMessageDialog($"Welcome {Username}", null);
            _regionManager.RequestNavigate("MainRegion", ViewsNames.ProductsView);
        }

        private bool CheckUsername()
        {
            return _validationService.IsRegexValid(Username, RegularExpressions.LETTERS_AND_NUMBERS_ONLY_PATTERN);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Username = "";
            Global.UserName = "";
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Global.UserName = Username;
        }
    }
}
