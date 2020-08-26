using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace ECommerce.Dialogs
{
    public class MessageDialogViewModel : BindableBase, IDialogAware
    {
        private string _message;

        public MessageDialogViewModel()
        {
            CloseDialogCommand = new DelegateCommand(CloseDialog);
        }

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public DelegateCommand CloseDialogCommand { get; }

        public event Action<IDialogResult> RequestClose;
        public string Title => "MessageDialog";

        private void CloseDialog()
        {
            var result = ButtonResult.OK;

            var param = new DialogParameters();
            param.Add("dialogParam", "Dialog Was Closed by User");

            RequestClose?.Invoke(new DialogResult(result, param));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }
    }
}
