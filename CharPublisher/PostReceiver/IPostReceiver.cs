using System;
using System.Collections.Generic;
using System.Text;
using Models;
using RabbitMQ.Client;

namespace ChatPublisher.PostReceiver
{
    public interface IPostReceiver
    {
        PostModel Post { get; }

        delegate void MessageReceivedEventHandler(object source, EventArgs args);
        
        event EventHandler<MessageEventArgs> MessageReceived;
        
        void Receive();
    }
}
