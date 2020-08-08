using System;
using System.IO;
using System.Threading;
using ChatSender.Logging;
using ChatSender.Post;
using Models;
using Services.Logging;

namespace ChatSender
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var streamWriter = File.AppendText("log.txt");
            var logger = new FileLogger(streamWriter);

            var publisher = new PostPublisherRabbit("localhost", logger);
            
            var postModel = new PostModel();
            IMessageBuilder messageBuilder = new ConsoleMessageBuilder();

            while (true)
            {
                messageBuilder.SendMessage(postModel);
                publisher.Publish(postModel);
            }
        }
    }
}
