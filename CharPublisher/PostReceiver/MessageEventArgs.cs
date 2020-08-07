using System;
using Models;

namespace ChatPublisher.PostReceiver
{
    public class MessageEventArgs : EventArgs
    {
        public PostModel Post { get; set; }
    }
}