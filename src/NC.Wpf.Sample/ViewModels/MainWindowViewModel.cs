using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NC.Wpf.Application.Contracts;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace NC.Wpf.Sample.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject, ISingletonDependency
    {
        public MainWindow _mainWindow;
        private readonly ISampleAppService _sampleAppService;

        public MainWindowViewModel(ISampleAppService sampleAppService)
        {
            _sampleAppService = sampleAppService;
        }

        [RelayCommand]
        public async void SampleServiceCall()
        {
            try
            {
                var result = await _sampleAppService.GetAsync();
                //var result = AsyncHelper.RunSync(() => _sampleAppService.GetAsync());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
