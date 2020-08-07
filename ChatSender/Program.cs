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
            var postModel = new PostModel 
                {
                    Title = "InterestingPost",
                    Text = "Content of the very very interesting post"
                };

            var publisher = new PostPublisherRabbit("localhost", logger);

            while (true)
            {
                publisher.Publish(postModel);
                Thread.Sleep(5000);
            }

            streamWriter.Dispose();
        }
    }
}
