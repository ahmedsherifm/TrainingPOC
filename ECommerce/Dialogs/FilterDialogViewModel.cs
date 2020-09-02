using ECommerce.DTOs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace ECommerce.Dialogs
{
    public class FilterDialogViewModel: BindableBase, IDialogAware
    {
        private ProductFilterDTO _productFilter;
        public ProductFilterDTO ProductFilter
        {
            get { return _productFilter; }
            set { SetProperty(ref _productFilter, value); }
        }

        public FilterDialogViewModel()
        {
            ApplyFilterCommand = new DelegateCommand(OnApplyFilter);
            CancelFilterCommand = new DelegateCommand(OnCancelFilterDialog);
        }

        public DelegateCommand ApplyFilterCommand { get; private set; }

        public DelegateCommand CancelFilterCommand { get; private set; }

        public string Title => "Filter Products";

        public event Action<IDialogResult> RequestClose;

        private void OnApplyFilter()
        {
            var result = ButtonResult.OK;

            var param = new DialogParameters();
            param.Add("productFilter", ProductFilter);

            RequestClose?.Invoke(new DialogResult(result, param));
        }

        private void OnCancelFilterDialog()
        {
            var result = ButtonResult.Cancel;

            RequestClose?.Invoke(new DialogResult(result));
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
            ProductFilter = parameters.GetValue<ProductFilterDTO>("productFilter");
        }
    }
}
