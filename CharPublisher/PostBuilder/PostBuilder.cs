using Models;

namespace ChatPublisher.PostBuilder
{
    public class PostBuilder : IPostBuilder
    {
        public PostModel BuildModel(string message)
        {
            var indexOfDivider = message.IndexOf('%');
            var title = message.Substring(0, indexOfDivider);
            var text= message.Substring(indexOfDivider + 1);
            
            return new PostModel{Title = title, Text = text};
        }
    }
}