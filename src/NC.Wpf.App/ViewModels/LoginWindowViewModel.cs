using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Volo.Abp.DependencyInjection;
using NC.Wpf.Core.Navigation.Regions;
using NC.Wpf.Framework.Mvvm;
using NC.Wpf.App.Views;
using NC.Wpf.Application.Contracts;

namespace NC.Wpf.App.ViewModels
{
    public partial class LoginWindowViewModel : ViewModelBase, IRegionMemberLifetime, ISingletonDependency
    {
        private readonly MainWindow _mainWindow;

        public ISampleAppService _sampleAppService { get; set; }

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _loginMessage;

        [ObservableProperty]
        private bool _loginLoading;

        public LoginWindowViewModel(MainWindow mainWindow, ISampleAppService sampleAppService)
        {
            _mainWindow = mainWindow;
            _sampleAppService = sampleAppService;
            UserName = "admin";
            Password = "admin";
            LoginLoading = false;
        }

        [RelayCommand]
        public async Task Login(Window loginWindow)
        {
            if (LoginLoading)
            {
                return;
            }
            LoginLoading = true;
            
            //var result = await _sampleAppService.GetAsync();
            
            if (Validate())
            {
                loginWindow.Close();
                _mainWindow.Show();
            }
        }

        private bool Validate()
        {
            if (UserName != "admin")
            {
                LoginMessage = "请输入有效的用户名！";
                return false;
            }

            if (Password != "admin")
            {
                LoginMessage = "请输入正确的密码！";
                return false;
            }
            return true;

        }

        public bool KeepAlive => false;
    }
}
