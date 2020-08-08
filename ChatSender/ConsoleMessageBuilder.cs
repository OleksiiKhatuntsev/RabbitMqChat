using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace ChatSender
{
    public class ConsoleMessageBuilder : IMessageBuilder
    {
        public void SendMessage(PostModel postModel)
        {
            Console.Write("Enter Title: ");
            postModel.Title = Console.ReadLine();
            Console.Write("Enter Text: ");
            postModel.Text = Console.ReadLine();
        }
    }
}
