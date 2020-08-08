using System.IO;
using ChatPublisher.PostReceiver;
using ChatSender.Logging;
using Models;

namespace ChatPublisher.PostPublisher
{
    public class FilePostPublisher : PostPublisher, IPostPublisher
    {
        private readonly StreamWriter _streamWriter;

        public FilePostPublisher(ILogger logger, StreamWriter streamWriter) : base(logger)
        {
            _streamWriter = streamWriter;
        }

        public void Publish(object sender, MessageEventArgs args)
        {
            _streamWriter.WriteLine(args.Post.Title + "\n");
            _streamWriter.WriteLine(args.Post.Text + "\n\n");
        }
    }
}