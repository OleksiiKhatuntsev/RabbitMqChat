using System.IO;
using ChatSender.Logging;

namespace Services.Logging
{
    public class FileLogger : ILogger
    {
        private readonly StreamWriter _streamWriter;

        public FileLogger(StreamWriter streamWriter)
        {
            _streamWriter = streamWriter;
        }

        public void LogInfo(string message)
        {
            WriteText($"Info: {message}");
        }

        public void LogWarning(string message)
        {
            WriteText($"Warning: {message}");
        }

        public void LogError(string message)
        {
            WriteText($"Error: {message}");
        }

        private void WriteText(string message)
        {
            _streamWriter.WriteLine(message);
        }
    }
}
