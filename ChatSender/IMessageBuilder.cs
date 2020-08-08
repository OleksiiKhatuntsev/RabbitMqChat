using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace ChatSender
{
    public interface IMessageBuilder
    {
        public void SendMessage(PostModel postModel);
    }
}
