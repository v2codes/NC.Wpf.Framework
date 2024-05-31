using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace NC.Wpf.Framework.Events
{
    /// <summary>
    /// 这是一个特殊的消息类型。
    /// 如果 Send 方法发送的是这个类（或它的子类），那么 Send 方法将拥有一个返回值，这个返回值就是消息的接收者回复的消息。
    /// 此时，消息的接收者也能够通过消息上的 Reply 方法回复消息的发送者。
    /// </summary>
    /// <typeparam name="TResponse">返回消息类型</typeparam>
    public class RequestMessageBase : RequestMessage<string>
    {
        /// <summary>
        /// 发送消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">发送消息内容</param>
        public RequestMessageBase(string message)
        {
            Message = message;
        }
    }
}
