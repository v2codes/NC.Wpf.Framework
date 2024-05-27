using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Volo.Abp.DependencyInjection;
using NC.Wpf.Core.Dialogs;
using NC.Wpf.Framework.Mvvm;
using CommunityToolkit.Mvvm.Input;

namespace NC.Wpf.ControlModule.ViewModels
{
    public partial class MessageDialogViewModel : ViewModelBase, IDialogAware, ITransientDependency
    {
        [ObservableProperty]
        private string _message;
        public DialogCloseListener RequestClose { get; set; }

        public MessageDialogViewModel()
        {
        }

        [RelayCommand]
        public void CloseDialog(string btnType)
        {
            var btnResult = btnType switch
            {
                "OK" => ButtonResult.OK,
                "Cancel" => ButtonResult.Cancel
            };
            var result = new DialogResult()
            {
                Result = btnResult,
                Parameters = { { "message", "Message Dialog 被关闭了！" } },
            };

            RequestClose.Invoke(result);
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
            var msg = parameters.GetValue<string>("message");
            Message = msg;
        }
    }
}
