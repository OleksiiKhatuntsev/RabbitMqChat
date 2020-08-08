using System;
using ChatPublisher.PostReceiver;
using ChatSender.Logging;
using Models;

namespace ChatPublisher.PostPublisher
{
    public class ConsolePostPublisher : PostPublisher, IPostPublisher
    {
        public ConsolePostPublisher(ILogger logger) : base(logger)
        {  }

        public void Publish(object sender, MessageEventArgs args)
        {
            Console.WriteLine($"Title: {args.Post.Title}");
            Console.WriteLine($"Text: {args.Post.Text}\n");
        }
    }
}