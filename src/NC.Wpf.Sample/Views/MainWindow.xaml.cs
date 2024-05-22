using NC.Wpf.Sample.ViewModels;
using System;
using System.Windows;

namespace NC.Wpf.Sample;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //private readonly HelloWorldService _helloWorldService;

    //public MainWindow(HelloWorldService helloWorldService)
    //{
    //_helloWorldService = helloWorldService;
    //    InitializeComponent();
    //}

    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        DataContext = mainWindowViewModel;
        InitializeComponent();
    }

    //protected override void OnContentRendered(EventArgs e)
    //{
    //HelloLabel.Content = _helloWorldService.SayHello();
    //}
}
