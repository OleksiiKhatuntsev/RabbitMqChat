using System;
using ChatSender.Logging;
using Models;

namespace ChatPublisher.PostPublisher
{
    public abstract class PostPublisher
    {
        protected readonly ILogger Logger;

        protected PostPublisher(ILogger logger)
        {
            Logger = logger;
        }
    }
}