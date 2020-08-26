using ECommerce.Core;
using ECommerce.Core.Constants;
using ECommerce.Main.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ECommerce.Main.ViewModels
{
    public class LoginViewModel: BindableBase, INotifyDataErrorInfo
    {
        private string _username;
        private IDialogService _dialogService;
        private readonly IValidationService _validationService;

        public LoginViewModel(IDialogService dialogService, IValidationService validationService)
        {
            LoginCommand = new DelegateCommand(OnSubmit);
            _dialogService = dialogService;
            _validationService = validationService;
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
                    "Please re-enter username without spaces or special characters", null);

                return;
            }

            _dialogService.ShowMessageDialog($"Welcome {Username}", null);
        }

        public bool HasErrors { get; set; } = false;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || (!HasErrors))
                return null;

            return new List<string>() { "Invalid username format" };
        }

        private bool CheckUsername()
        {
            HasErrors = !_validationService.IsRegexValid(Username, RegularExpressions.LETTERS_AND_NUMBERS_ONLY_PATTERN);

            if (HasErrors)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Username)));
                return false;
            }

            return true;
        }
    }
}
