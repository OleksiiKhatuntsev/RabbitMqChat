using System.Collections.Generic;
using System.Text;
using ChatPublisher.PostReceiver;
using Models;

namespace ChatPublisher.PostPublisher
{
    public interface IPostPublisher
    {
        public void Publish(object sender, MessageEventArgs args);
    }
}
