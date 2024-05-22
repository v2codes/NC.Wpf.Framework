using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NC.Wpf.Application.Contracts;
using Volo.Abp.DependencyInjection;

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
        public void SampleServiceCall()
        {
            try
            {
                var result = _sampleAppService.GetAsync().Result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
