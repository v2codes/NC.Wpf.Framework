using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Volo.Abp.DependencyInjection;
using NC.Wpf.Framework.Mvvm;
using NC.Wpf.Framework.Events;

namespace NC.Wpf.HomeModule.ViewModels
{
    public partial class HomeViewViewModel : ViewModelBase, ITransientDependency
    {
        [ObservableProperty]
        private string _helloWorld;

        [ObservableProperty]
        private string _responseOfRequestMessage;

        public HomeViewViewModel(IServiceProvider serviceProvider)
        {
            HelloWorld = "Hello Home Module！";
        }

        [RelayCommand]
        public void SendMessageToMainView()
        {
            // ref: https://www.cnblogs.com/aierong/p/17318525.html
            // Send发送消息
            Messenger.Send("Home模块：我发送了普通消息！", "ModuleMessageToken");
            //WeakMessenger.Send("Home模块：我发送了普通消息！", "ModuleMessageToken");

            // 特别注意:直接传递值,只可以是引用类型,值类型不可以编译成功的(例如:下面2句不行)
            // WeakMessenger.Send<int , string>( 11 , "token_1" );
            // WeakMessenger.Send<bool , string>( true  , "token_1" );

            // 官方推荐用ValueChangedMessage封装数据传递
            // 建议发送消息时都带上token，便于接收方过滤数据 
            // WeakMessenger.Send<ValueChangedMessage<string>, string>(new ValueChangedMessage<string>("我是Home模块传递的消息"), "ModuleMessageToken");

            // Send发送 一个复杂数据 
            // var message = new MyUserMessage() { Age = 18, UserName = "qq" };
            // WeakMessenger.Send<ValueChangedMessage<MyUserMessage>, string>(new ValueChangedMessage<MyUserMessage>(message), "token_1");
        }

        [RelayCommand]
        public void SendRequestMessageToMainView()
        {
            var response = Messenger.Send(new RequestMessageBase("Home模块：我发送了需要响应的消息！"), "ModuleMessageToken");
            if (response.HasReceivedResponse)
            {
                ResponseOfRequestMessage = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - {response.Response}";
            }
        }
    }
}
