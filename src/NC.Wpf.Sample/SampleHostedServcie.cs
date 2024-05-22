using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace NC.Wpf.Sample
{
    public class SampleHostedServcie<TApplication, TWindow> : IHostedService where TApplication : System.Windows.Application where TWindow : Window
    {
        private readonly TApplication _application;
        private readonly TWindow _window;
        private readonly IHostApplicationLifetime _appLifetime;

        public SampleHostedServcie(TApplication app,
                                   TWindow window,
                                   IHostApplicationLifetime appLifetime)
        {
            _application = app;
            _window = window;
            _appLifetime = appLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // 处理wpf应用退出时，停止主机
            _application.Exit += App_Exit;

            _application.Run(_window);
            return Task.CompletedTask;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            //_app.Shutdown();
            _appLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //_app.Shutdown();
            _appLifetime.StopApplication();
            return Task.CompletedTask;
        }
    }
}
