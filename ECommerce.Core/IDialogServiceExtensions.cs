using Prism.Services.Dialogs;
using System;

namespace ECommerce.Core
{
    public static class IDialogServiceExtensions
    {
        public static void ShowMessageDialog(this IDialogService dialogService, string message, Action<IDialogResult> callBack)
        {
            var param = new DialogParameters();
            param.Add("message", message);

            dialogService.ShowDialog("MessageDialog", param, callBack);
        }
    }
}
