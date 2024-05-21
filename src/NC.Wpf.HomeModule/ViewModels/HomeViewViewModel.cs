using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using Volo.Abp.DependencyInjection;
using NC.Wpf.Framework.Mvvm;

namespace NC.Wpf.HomeModule.ViewModels
{
    public partial class HomeViewViewModel : ViewModelBase, ITransientDependency
    {
        [ObservableProperty]
        private string _helloWorld;

        public HomeViewViewModel(IServiceProvider serviceProvider)
        {
            HelloWorld = "Hello Home Module！";
        }

        [RelayCommand]
        public void SendMessageToMainView()
        {
            // ref: https://www.cnblogs.com/aierong/p/17318525.html
            // Send发送消息
            WeakReferenceMessenger.Default.Send<string,string>("我是Home模块传递的消息", "ModuleMessageToken");

            // 特别注意:直接传递值,只可以是引用类型,值类型不可以编译成功的(例如:下面2句不行)
            // WeakReferenceMessenger.Default.Send<int , string>( 11 , "token_1" );
            // WeakReferenceMessenger.Default.Send<bool , string>( true  , "token_1" );

            // 官方推荐用ValueChangedMessage封装数据传递
            // 建议发送消息时都带上token，便于接收方过滤数据 
            // WeakReferenceMessenger.Default.Send<ValueChangedMessage<string>, string>(new ValueChangedMessage<string>("我是Home模块传递的消息"), "ModuleMessageToken");

            // Send发送 一个复杂数据 
            // var message = new MyUserMessage() { Age = 18, UserName = "qq" };
            // WeakReferenceMessenger.Default.Send<ValueChangedMessage<MyUserMessage>, string>(new ValueChangedMessage<MyUserMessage>(message), "token_1");

        }
    }
}
